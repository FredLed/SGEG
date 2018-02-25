using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Repository.SQLRepo
{
    static class SQLDbHelper
    {
        public const string ProductTable = "Products";
        public const string UserTable = "Users";
        public const string CategoryTable = "Categories";

        static public T GetValueOrDefault<T>(this SqlDataReader dr, string colName, T defaultValue)
        {
            return GetValueOrDefault(dr, dr.GetOrdinal(colName), defaultValue);
        }

        static public T GetValueOrDefault<T>(this SqlDataReader dr, int colNumber, T defaultValue)
        {
            return (dr.IsDBNull(colNumber)) ? (T)Convert.ChangeType(defaultValue, typeof(T)) : (T)dr.GetValue(colNumber);
        }

        static public Guid GetGuid(this SqlDataReader dr, string colName)
        {
            return Guid.Parse(GetValueOrDefault(dr, colName, Guid.Empty.ToString()));
        }

        static public DateTime GetDateTime(this SqlDataReader dr, string colName)
        {
            return GetValueOrDefault(dr, colName, DateTime.Now);
        }

        static public double GetDouble(this SqlDataReader dr, string colName)
        {
            return GetValueOrDefault(dr, dr.GetOrdinal(colName), 0.00);
        }
    }
}
