using Dapper.AmbientContext;

namespace ActivitySignUp.Repositories
{
    /// <summary>
    /// the base repository
    /// </summary>
    public abstract class BaseRepository
    {

        /// <summary>
        /// the dapper ambient context
        /// </summary>
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="ambientDbContextLocator"></param>
        protected BaseRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        /// <summary>
        /// returns the ambient context on get
        /// </summary>
        protected IAmbientDbContextQueryProxy DbContext
        {
            get { return _ambientDbContextLocator.Get(); }
        }
    }
}
