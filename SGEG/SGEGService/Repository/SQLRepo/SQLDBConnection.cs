using SGEGService.Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Repository.SQLRepo
{
    public abstract class SQLDbConnection : IDbConnection
    {
        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\LL\Projets\SGEG\Code\SGEG\SGEGService\DB\SGEGData.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }
    }
}
