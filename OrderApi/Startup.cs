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
using OrderService.Infrastructure;
using OrderService.IntegrationEventHandler;

namespace OrderService
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "OrderApi", Version = "v1"});
            });

            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDatabaseDeveloperPageExceptionFilter(); 
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCreatedEventHandler>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("amqp://guest:guest@localhost:5672");
                    cfg.ReceiveEndpoint("orderCreated", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedEventHandler>(context); 
                    });
                });
            });
            
            services.AddMassTransitHostedService();
            services.AddAutoMapper(typeof(Startup));
        }
        
        class EventConsumer :
            IConsumer<ValueEntered>
        {
            private readonly ILogger<EventConsumer> _logger;

            public EventConsumer(ILogger<EventConsumer> logger)
            {
                _logger = logger;
            }

            public Task Consume(ConsumeContext<ValueEntered> context)
            {
                _logger.LogInformation("Value: {Value}", context.Message.Value);
                return Task.CompletedTask;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
    
    public interface ValueEntered 
    { 
        string Value { get; }
    }
}