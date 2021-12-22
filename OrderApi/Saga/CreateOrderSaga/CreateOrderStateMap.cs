using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderService.Saga.CreateOrderSaga
{
    public class CreateOrderStateMap
        :SagaClassMap<CreateOrderState>
    {
        protected override void Configure(EntityTypeBuilder<CreateOrderState> entity, ModelBuilder model)
        {
            entity.Property(x => x.CurrentState).HasMaxLength(64);
            entity.Property(x => x.OrderId);

            entity.Property(x => x.RowVersion).IsRowVersion(); 
        } 
    }
}