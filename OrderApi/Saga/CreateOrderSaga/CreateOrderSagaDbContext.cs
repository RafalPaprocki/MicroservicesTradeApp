using System;
using System.Collections.Generic;
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Saga.CreateOrderSaga
{
    public class CreateOrderSagaDbContext : SagaDbContext
    {
        public CreateOrderSagaDbContext(DbContextOptions<CreateOrderSagaDbContext> options) 
            : base(options)
        {
            Console.WriteLine("This method is invoked  ");
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get
            {
                yield return new CreateOrderStateMap();
            }
        }
    }
}