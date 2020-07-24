using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ActivitySignUp.Repositories.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
