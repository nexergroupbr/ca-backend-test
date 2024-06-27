using DTO = Domain.Dtos.CustomerDTO;
using IRepository = Domain.Abstractions.Repositories.ICustomerRepository;
using TEntity = Domain.Entities.Customer;

namespace Services;

public class CustomerService(IRepositoryWrapper RepositoryWrapper,
                             IMapper mapper,
                             IValidator<DTO> dtoValidator)
    : BaseService,
    ICustomerService
{
    protected IRepository Repository => RepositoryWrapper.Customer;

    public async Task<ServiceResult<TModel>> GetByIdAsync<TModel>(Guid id,
                                                                  CancellationToken cancellationToken = default)
    {
        TModel? value = await Repository.GetByIdAsync<TModel>(id, cancellationToken);

        if (value is null)
        {
            return NotFound();
        }

        return Success(value);
    }

    public async Task<ServiceResult<List<TModel>>> GetAllAsync<TModel>(Pagination pagination,
                                                                       CancellationToken cancellationToken = default)
    {
        pagination ??= new();

        List<TModel> values = await Repository.GetAllAsync<TModel>(pagination, cancellationToken);

        return Success(values);
    }

    public async Task<ServiceResult<TEntity>> CreateAsync(DTO dto,
                                                          CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await dtoValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationError(validationResult);
        }

        TEntity entity = mapper.Map<TEntity>(dto);
        Repository.Create(entity);
        await RepositoryWrapper.SaveAsync(cancellationToken);

        return Success(entity);
    }

    public async Task<ServiceResult> UpdateAsync(Guid id,
                                                 DTO dto,
                                                 CancellationToken cancellationToken = default)
    {
        TEntity? entity = await Repository.GetByIdAsync<TEntity>(id, cancellationToken);
        if (entity is null)
        {
            return NotFound();
        }

        ValidationResult validationResult = await dtoValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationError(validationResult);
        }

        mapper.Map(dto, entity);
        entity.UpdatedAt = DateTime.UtcNow;
        Repository.Update(entity);
        await RepositoryWrapper.SaveAsync(cancellationToken);

        return Success();
    }

    public async Task<ServiceResult> DeleteAsync(Guid id,
                                                 CancellationToken cancellationToken = default)
    {
        if (!(await Repository.ExistsAsync(id, cancellationToken)))
        {
            return NotFound();
        }

        await Repository.DeleteAsync(id, cancellationToken);

        return Success();
    }
}
