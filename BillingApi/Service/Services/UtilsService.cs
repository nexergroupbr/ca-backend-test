using BillingApi.Service.Interfaces;

namespace BillingApi.Service.Services
{
    public class UtilsService : IUtilsService
    {

        public TViewModel ConvertToViewModel<TEntity, TViewModel>(TEntity entity)
        {
            var viewModel = Activator.CreateInstance<TViewModel>();

            foreach (var entityProperty in typeof(TEntity).GetProperties())
            {
                var viewModelProperty = typeof(TViewModel).GetProperty(entityProperty.Name);
                if (viewModelProperty != null && viewModelProperty.CanWrite)
                {
                    viewModelProperty.SetValue(viewModel, entityProperty.GetValue(entity));
                }
            }

            return viewModel;
        }

        public TEntity ConvertToEntity<TEntity, TViewModel>(TViewModel viewModel) where TEntity : new()
        {
            var entity = new TEntity();
            foreach (var viewModelProperty in typeof(TViewModel).GetProperties())
            {
                var entityProperty = typeof(TEntity).GetProperty(viewModelProperty.Name);

                if (entityProperty != null && entityProperty.CanWrite)
                {
                    var value = viewModelProperty.GetValue(viewModel);
                    entityProperty.SetValue(entity, value);
                }
            }
            return entity;
        }
    }
}
