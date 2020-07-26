using Dapper.AmbientContext;
using Dapper.AmbientContext.Storage;

namespace ActivitySignUp.Services
{
    public class BaseService
    {

        //protected IAmbientDbContextQueryProxy DbContext;
        protected IAmbientDbContextFactory ContextFactory;
        //protected IAmbientDbContextLocator ContextLocator;

        public BaseService(
            IAmbientDbContextFactory factory//,
            //IAmbientDbContextLocator locator
            )
        {
            ContextFactory = factory;
            //ContextLocator = locator;
            AmbientDbContextStorageProvider.SetStorage(new AsyncLocalContextStorage());

            ContextFactory.Create();

            //DbContext = ContextLocator.Get();
        }

    }
}
