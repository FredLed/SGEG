using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Infrastructure.SqlRepository
{
    public abstract class SqlRepositoryConnection
    {
        public string ConnectionString { get; set; }
    }
}
