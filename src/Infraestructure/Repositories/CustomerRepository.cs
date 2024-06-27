namespace Infraestructure.Repositories;

public class CustomerRepository(DataContext repositoryContext, IMapper mapper)
    : EntityRepository<Customer>(repositoryContext, mapper),
    ICustomerRepository
{

}
