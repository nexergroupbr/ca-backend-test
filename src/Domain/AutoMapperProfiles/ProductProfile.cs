using AutoMapper;

namespace Domain.AutoMapperProfiles;

public class ProductProfile
    : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductDTO, Product>();
        CreateMap<Product, ProductViewModel>();
    }
}
