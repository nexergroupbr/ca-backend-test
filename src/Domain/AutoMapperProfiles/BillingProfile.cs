using AutoMapper;

namespace Domain.AutoMapperProfiles;

public class BillingProfile
    : Profile
{
    public BillingProfile()
    {
        CreateMap<Billing, BillingViewModel>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(o => o.Customer.Name));

        CreateMap<BillingLine, BillingLineViewModel>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(o => o.Product.Name));
    }
}
