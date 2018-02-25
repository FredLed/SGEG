using SGEGService.Model;
using SGEGService.Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Repository.SQLRepo
{
    public class SQLProductRepo : SQLDbConnection, IProductRepo
    {

        public List<IProduct> GetAllProducts()
        {
            List<IProduct> products = new List<IProduct>();
            string sql = "SELECT * FROM " + SQLDbHelper.ProductTable;

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);

                    con.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        products.Add(ParseProduct(dr));
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

            return products;
        }

        public List<IProduct> Products => GetAllProducts();

        public IProduct GetProductByID(Guid id)
        {
            IProduct product = new Product();
            string sql = "SELECT * FROM " + SQLDbHelper.ProductTable + " WHERE ID = @ID";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("ID", id);

                    con.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        product = ParseProduct(dr);
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

            return product;
        }

        public bool DeleteProductByID(Guid ID)
        {
            string sql = "DELETE FROM " + SQLDbHelper.ProductTable + " WHERE ID = @ID";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("ID", ID);

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

        public bool SaveProduct(IProduct product)
        {
            string sql = "INSERT INTO " + SQLDbHelper.ProductTable + " (ID,Name,MSRP,Description,CreationDate,CUP) "
                            + " VALUES (@ID,@Name,@MSRP,@Description,@CreationDate,@CUP)";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("ID", product.ID);
                    command.Parameters.AddWithValue("Name", product.Name);
                    command.Parameters.AddWithValue("MSRP", product.MSRP);
                    command.Parameters.AddWithValue("Description", product.Description);
                    command.Parameters.AddWithValue("CreationDate", product.CreationDate);
                    command.Parameters.AddWithValue("CUP", product.CUP);

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

        private Product ParseProduct(SqlDataReader dr)
        {
            try
            {
                return new Product()
                {
                    ID = SQLDbHelper.GetGuid(dr, "ID"),
                    Name = SQLDbHelper.GetValueOrDefault(dr, "Name", ""),
                    MSRP = SQLDbHelper.GetDouble(dr, "MSRP"),
                    CreationDate = SQLDbHelper.GetDateTime(dr, "CreationDate"),
                    CUP = SQLDbHelper.GetValueOrDefault(dr, "CUP", ""),
                    Description = SQLDbHelper.GetValueOrDefault(dr, "Description", "")
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
