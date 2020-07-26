﻿using Dapper.AmbientContext;

namespace ActivitySignUp.Repositories
{
    public class BaseRepository
    {

        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        public BaseRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        protected IAmbientDbContextQueryProxy DbContext
        {
            get { return _ambientDbContextLocator.Get(); }
        }
    }
}
