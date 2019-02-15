using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using WebApp.BL;
using WebApp.BL.Interface;

namespace WebApp.Infrastructure.SqlRepo
{
    public class SqlItemRepo : IItemRepo
    {
        public IEnumerable<IItem> GetAllItems()
        {
            List<IItem> items = new List<IItem>();
            string sql = "SELECT * FROM " + SqlDbHelper.ItemTable;

            using (var con = new SqlConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);

                    con.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        items.Add(ParseItem(dr));
                    }

                    dr.Close();
                    command.Dispose();
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return items;
        }

        public List<IItem> Items => GetAllItems().ToList();

        public bool DeleteItemById(Guid id)
        {
            string sql = "DELETE FROM " + SqlDbHelper.ItemTable + " WHERE Id = @Id";

            using (var con = new SqlConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("Id", id);

                    con.Open();
                    int rowCount = command.ExecuteNonQuery();
                    command.Dispose();
                    con.Close();

                    if (rowCount == 0)
                    {
                        return false;
                    }


                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public bool UpdateItem(IItem item)
        {
            string sql = "UPDATE " + SqlDbHelper.ItemTable
                        + " SET ProductId = @productId, Cost = @cost, SerialNumber = @serialNumber, ReceptionDate = @receptionDate "
                            + " WHERE Id = @id";

            using (var con = new SqlConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("id", item.Id);
                    command.Parameters.AddWithValue("productId", item?.Product?.Id ?? Guid.Empty);
                    command.Parameters.AddWithValue("cost", item.Cost);
                    command.Parameters.AddWithValue("serialNumber", item.SerialNumber);
                    command.Parameters.AddWithValue("receptionDate", item.ReceptionDate);

                    con.Open();
                    int rowCount = command.ExecuteNonQuery();

                    if (rowCount == 0)
                    {
                        return false;
                    }

                    command.Dispose();
                    con.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public IItem GetItemById(Guid id)
        {
            IItem item = null;
            string sql = "SELECT * FROM " + SqlDbHelper.ItemTable + " WHERE Id = @id";

            using (var con = new SqlConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("id", id);

                    con.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        item = ParseItem(dr);
                    }

                    dr.Close();
                    command.Dispose();
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return item;
        }

        public bool SaveItem(IItem item)
        {
            if (GetItemById(item.Id) == null)
            {
                return InsertItem(item);
            }
            else
            {
                return UpdateItem(item);
            }
        }

        private bool InsertItem(IItem item)
        {
            string sql = "INSERT INTO " + SqlDbHelper.ItemTable + " (Id,ProductId,Cost,SerialNumber,CreationDate,ReceptionDate) "
                            + " VALUES (@Id,@productId,@cost,@serialNumber,@creationDate,@receptionDate)";

            using (var con = new SqlConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("Id", item.Id);
                    command.Parameters.AddWithValue("productId", item?.Product?.Id ?? Guid.Empty);
                    command.Parameters.AddWithValue("cost", item.Cost);
                    command.Parameters.AddWithValue("serialNumber", item.SerialNumber);
                    command.Parameters.AddWithValue("creationDate", item.CreationDate);
                    command.Parameters.AddWithValue("receptionDate", item.ReceptionDate);

                    con.Open();
                    int rowCount = command.ExecuteNonQuery();

                    if (rowCount == 0)
                    {
                        return false;
                    }

                    command.Dispose();
                    con.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public IEnumerable<IItem> GetItemsByProductId(Guid id)
        {
            List<IItem> items = new List<IItem>();
            string sql = "SELECT * FROM " + SqlDbHelper.ItemTable + " WHERE ProductId = @productId";

            using (var con = new SqlConnection())
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("productId", id);

                    con.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        items.Add(ParseItem(dr));
                    }

                    dr.Close();
                    command.Dispose();
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return items;
        }

        public Item ParseItem(SqlDataReader dr)
        {
            try
            {
                var productId = SqlDbHelper.GetGuid(dr, "ProductId");
                var productRepo = new SqlProductRepo();
                var product = productRepo.GetProductById(productId);

                return new Item()
                {
                    Id = SqlDbHelper.GetGuid(dr, "Id"),
                    Product = product,
                    Cost = SqlDbHelper.GetDouble(dr, "Cost"),
                    CreationDate = SqlDbHelper.GetDateTime(dr, "CreationDate"),
                    ReceptionDate = SqlDbHelper.GetDateTime(dr, "ReceptionDate"),
                    SerialNumber = SqlDbHelper.GetValueOrDefault(dr, "SerialNumber", "")
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}