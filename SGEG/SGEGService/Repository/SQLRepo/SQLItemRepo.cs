using SGEGService.Model.Interface;
using SGEGService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SGEGService.Repository.SQLRepo
{
    public class SQLItemRepo : SQLDbConnection, IItemRepo
    {
        public IEnumerable<IItem> GetAllItems()
        {
            List<IItem> items = new List<IItem>();
            string sql = "SELECT * FROM " + SQLDbHelper.ItemTable;

            using (var con = Connection)
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

        public bool DeleteItemByID(Guid id)
        {
            string sql = "DELETE FROM " + SQLDbHelper.ItemTable + " WHERE ID = @ID";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("ID", id);

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

        public bool SaveItem(IItem item)
        {
            string sql = "INSERT INTO " + SQLDbHelper.ItemTable + " (ID,ProductID,Cost,SerialNumber,CreationDate,ReceptionDate) "
                            + " VALUES (@ID,@productID,@cost,@serialNumber,@creationDate,@receptionDate)";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("ID", item.ID);
                    command.Parameters.AddWithValue("productID", item?.Product?.ID ?? Guid.Empty);
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

        public Item ParseItem(SqlDataReader dr)
        {
            try
            {
                var productID = SQLDbHelper.GetGuid(dr, "ProductID");
                var productRepo = new SQLProductRepo();
                var product = productRepo.GetProductByID(productID);

                return new Item()
                {
                    ID = SQLDbHelper.GetGuid(dr, "ID"),
                    Product = product,
                    Cost = SQLDbHelper.GetDouble(dr, "Cost"),
                    CreationDate = SQLDbHelper.GetDateTime(dr, "CreationDate"),
                    ReceptionDate = SQLDbHelper.GetDateTime(dr, "ReceptionDate"),
                    SerialNumber = SQLDbHelper.GetValueOrDefault(dr, "SerialNumber", "")
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
