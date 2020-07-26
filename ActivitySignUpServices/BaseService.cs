using Dapper.AmbientContext;
using Dapper.AmbientContext.Storage;

namespace ActivitySignUp.Services
{
    /// <summary>
    /// the base service
    /// </summary>
    public abstract class BaseService
    {

        protected IAmbientDbContextFactory ContextFactory;

        protected BaseService(
            IAmbientDbContextFactory factory
            )
        {
            AmbientDbContextStorageProvider.SetStorage(new AsyncLocalContextStorage());
            ContextFactory = factory;

            ContextFactory.Create();
        }

    }
}
