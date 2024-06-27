using AutoMapper;

namespace Domain.AutoMapperProfiles;

public class CustomerProfile
    : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerDTO, Customer>();
        CreateMap<Customer, CustomerViewModel>();
    }
}
