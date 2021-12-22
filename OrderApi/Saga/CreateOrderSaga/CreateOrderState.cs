using System;
using Automatonymous;

namespace OrderService.Saga.CreateOrderSaga
{
    public class CreateOrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } 
        public int OrderId { get; set; } 
        public byte[] RowVersion { get; set; }
    }
}