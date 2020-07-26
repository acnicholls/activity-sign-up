using Dapper.AmbientContext;

namespace ActivitySignUp.Repositories
{
    public abstract class BaseRepository
    {

        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        protected BaseRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        protected IAmbientDbContextQueryProxy DbContext
        {
            get { return _ambientDbContextLocator.Get(); }
        }
    }
}
