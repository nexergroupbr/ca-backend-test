using Domain.Entities;

namespace Services.AutoMapperProfiles;

public class BillingProfile
    : Profile
{
    public BillingProfile()
    {
        CreateMap<ExternalApiClient.Models.Billing, Billing>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(o => o.TotalAmount))
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(o => o.Customer.Id));

        CreateMap<ExternalApiClient.Models.BillingLine, BillingLine>();
    }
}
