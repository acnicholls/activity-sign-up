using Dapper.AmbientContext;
using Dapper.AmbientContext.Storage;

namespace ActivitySignUp.Services
{
    /// <summary>
    /// the base service
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// the ambient context factory
        /// </summary>
        protected IAmbientDbContextFactory ContextFactory;

        /// <summary>
        /// the basic ctor
        /// </summary>
        /// <param name="factory">the ambient context factory</param>
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
