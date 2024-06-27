namespace Infraestructure.Repositories;

public class ProductRepository(DataContext repositoryContext, IMapper mapper)
    : EntityRepository<Product>(repositoryContext, mapper),
    IProductRepository
{

}
