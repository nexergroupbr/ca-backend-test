using AutoMapper;
using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Models.Response;
using Ca.Backend.Test.Application.Models.Response.Api;
using Ca.Backend.Test.Domain.Entities;

namespace Ca.Backend.Test.Application.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CustomerRequest, CustomerEntity>();
        CreateMap<CustomerEntity, CustomerResponse>();

        CreateMap<ProductRequest, ProductEntity>();
        CreateMap<ProductEntity, ProductResponse>();

        CreateMap<BillingRequest, BillingEntity>();
        CreateMap<BillingEntity, BillingResponse>();

        CreateMap<BillingLineRequest, BillingLineEntity>();
        CreateMap<BillingLineEntity, BillingLineResponse>();

        CreateMap<CustomerApiResponse, CustomerEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); 

        CreateMap<BillingApiResponse, BillingEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForMember(dest => dest.Lines, opt => opt.MapFrom(src => src.Lines));

        CreateMap<LineApiResponse, BillingLineEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}

