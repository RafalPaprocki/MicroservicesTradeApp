using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderConfirmationApi.Consumer;

namespace OrderConfirmationApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<OrderConfirmationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AcceptOrderConsumer>();
                x.AddConsumer<OrderAcceptedConsumer>();
                x.AddConsumer<OrderRejectedConsumer>();
                x.AddConsumer<OrderSubmittedConsumer>();
                
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("amqp://guest:guest@localhost:5672");
                    cfg.ReceiveEndpoint("acceptOrder", e =>
                    {
                        e.ConfigureConsumer<AcceptOrderConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("orderAccepted", e =>
                    {
                        e.ConfigureConsumer<OrderAcceptedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("orderRejected", e =>
                    {
                        e.ConfigureConsumer<OrderRejectedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("orderSubmitted", e =>
                    {
                        e.ConfigureConsumer<OrderSubmittedConsumer>(context);
                    });
                });
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "OrderConfirmationApi", Version = "v1"});
            });

            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderConfirmationApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}