using System;
using System.Data.SqlClient;

namespace WebApp.Infrastructure.SqlRepository
{
    static class SqlDbHelper
    {
        public const string ProductTable = "Product";
        public const string CategoryTable = "Category";
        public const string ItemTable = "Item";

        public static T GetValueOrDefault<T>(this SqlDataReader dr, string colName, T defaultValue)
        {
            return dr.GetValueOrDefault(dr.GetOrdinal(colName), defaultValue);
        }

        public static T GetValueOrDefault<T>(this SqlDataReader dr, int colNumber, T defaultValue)
        {
            return dr.IsDBNull(colNumber) ? (T)Convert.ChangeType(defaultValue, typeof(T)) : (T)dr.GetValue(colNumber);
        }

        public static Guid GetGuid(this SqlDataReader dr, string colName)
        {
            return Guid.Parse(dr.GetValueOrDefault(colName, Guid.Empty.ToString()));
        }

        public static DateTime GetDateTime(this SqlDataReader dr, string colName)
        {
            return dr.GetValueOrDefault(colName, DateTime.Now);
        }

        public static double GetDouble(this SqlDataReader dr, string colName)
        {
            return dr.GetValueOrDefault(dr.GetOrdinal(colName), 0.00);
        }
    }
}