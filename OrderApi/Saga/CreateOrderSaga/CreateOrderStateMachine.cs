using System;
using System.Net.Mime;
using Automatonymous;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.Command;
using OrderService.Controllers;
using OrderService.IntegrationsEvent;

namespace OrderService.Saga.CreateOrderSaga
{
    public class CreateOrderStateMachine : MassTransitStateMachine<CreateOrderState>
    {
        private readonly ILogger<OrderController> _logger;
        public CreateOrderStateMachine()
        {
            _logger = LoggerFactory.Create(b => b.AddConsole()).CreateLogger<OrderController>();
            
            InstanceState(x => x.CurrentState);
            ConfigureCorrelationIds();
            ConfigureSagaBehavior();
        }

        public State Submitted { get; private set; }
        public State Created { get; private set; }
        public State Authorized { get; private set; }
        public State Rejected { get; private set; }
        public State Accepted { get; private set; }

        public Event<OrderSubmitted> OrderSubmitted { get; private set; }
        public Event<OrderCreated> OrderCreated { get; private set; }
        public Event<OrderAuthorized> OrderAuthorized { get; private set; }
        public Event<OrderRejected> OrderRejected { get; private set; }
        public Event<OrderAccepted> OrderAccepted { get; private set; }
        
        private void ConfigureCorrelationIds()
        {
            Event(() => OrderAuthorized, e => e
                .CorrelateBy((instance, context) => instance.OrderId == context.Message.OrderId)
                .SelectId(x => NewId.NextGuid()));

            Event(() => OrderAccepted, e => e
                .CorrelateBy((instance, context) => instance.OrderId == context.Message.OrderId)
                .SelectId(x => NewId.NextGuid()));

            Event(() => OrderSubmitted, e => e
                .CorrelateBy((instance, context) => instance.OrderId == context.Message.OrderId)
                .SelectId(x => NewId.NextGuid()));
            
            Event(() => OrderRejected, e => e
                .CorrelateBy((instance, context) => instance.OrderId == context.Message.OrderId)
                .SelectId(x => NewId.NextGuid()));

            Event(() => OrderCreated, e => e
                .CorrelateBy((instance, context) => instance.OrderId == context.Message.OrderId)
                .SelectId(x => NewId.NextGuid()));
        }

        private void ConfigureSagaBehavior()
        {
            Initially(
                When(OrderSubmitted)
                    .SendAsync(new Uri("queue:AuthorizeOrder"), context => context.Init<AuthorizeOrder>(new
                    {
                        OrderId = context.Instance.OrderId 
                    }))
                    .TransitionTo(Submitted));

            During(Submitted,
                When(OrderAuthorized) 
                    .SendAsync(new Uri("queue:AcceptOrder"), context => context.Init<AcceptOrder>(new
                    {
                        OrderId = context.Instance.OrderId
                    }))
                    .TransitionTo(Authorized));
            
            During(Submitted, 
                When(OrderRejected)
                    .TransitionTo(Rejected)
                    .Finalize());
            
            During(Authorized, 
                When(OrderAccepted)
                    .SendAsync(new Uri("queue:CreateOrder"), context => context.Init<CreateOrder>(new
                    {
                        OrderId = context.Instance.OrderId
                    }))
                    .TransitionTo(Accepted));
            
            During(Authorized,
                When(OrderRejected)
                    .TransitionTo(Rejected)
                    .Finalize());

            DuringAny(
                When(OrderCreated)
                    .TransitionTo(Created)
                    .Finalize());

            SetCompletedWhenFinalized();
        }
    }
}

// OrderSubmitted -> OrderAuthorized -> OrderAccepted -> OrderCreated
//                -> OrderRejected   -> OrderRejected
//
//
// OrderCancelStarted -> OrderAuthorized -> OrderCancelled
//                    -> OrderRejected   -> OrderCancelledRejected   
//
//