using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SGEGService.Model.Interface
{
    interface IDbConnection
    {
        SqlConnection Connection { get; }
    }
}
