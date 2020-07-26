using Dapper.AmbientContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitySignUp.RespositoryTests
{
    public abstract class AbstractTest
    {

        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        protected AbstractTest(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        protected IAmbientDbContextQueryProxy DbContext
        {
            get { return _ambientDbContextLocator.Get(); }
        }

    }
}
