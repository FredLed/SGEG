using System;
using System.Data.SqlClient;

namespace WebApp.Infrastructure.SqlRepo
{
    static class SqlDbHelper
    {
        public const string ProductTable = "Products";
        public const string CategoryTable = "Categories";
        public const string ItemTable = "Items";

        public static T GetValueOrDefault<T>(this SqlDataReader dr, string colName, T defaultValue)
        {
            return GetValueOrDefault(dr, dr.GetOrdinal(colName), defaultValue);
        }

        public static T GetValueOrDefault<T>(this SqlDataReader dr, int colNumber, T defaultValue)
        {
            return (dr.IsDBNull(colNumber)) ? (T)Convert.ChangeType(defaultValue, typeof(T)) : (T)dr.GetValue(colNumber);
        }

        public static Guid GetGuid(this SqlDataReader dr, string colName)
        {
            return Guid.Parse(GetValueOrDefault(dr, colName, Guid.Empty.ToString()));
        }

        public static DateTime GetDateTime(this SqlDataReader dr, string colName)
        {
            return GetValueOrDefault(dr, colName, DateTime.Now);
        }

        public static double GetDouble(this SqlDataReader dr, string colName)
        {
            return GetValueOrDefault(dr, dr.GetOrdinal(colName), 0.00);
        }
    }
}