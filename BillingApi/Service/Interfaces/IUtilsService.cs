namespace BillingApi.Service.Interfaces
{
    public interface IUtilsService
    {
        TViewModel ConvertToViewModel<TEntity, TViewModel>(TEntity entity);
        TEntity ConvertToEntity<TEntity, TViewModel>(TViewModel viewModel) where TEntity : new();
    }
}
