using AutoMapper;
using OrderService.Dto;
using OrderService.IntegrationsEvent;
using OrderService.Model;

namespace OrderService.MappingConfiguration
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>();
            CreateMap<Order, OrderCreatedEvent>()
                .ForMember(dest => dest.Status, 
                    opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.OrderId,
                    opt => opt.MapFrom(src => src.Id) );
            CreateMap<OrderCreateDto, Order>();
        }
    }
}