using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using WebApp.BL;
using WebApp.BL.Interface;

namespace WebApp.Infrastructure.SqlRepo
{
    public class SqlProductRepo : IProductRepo
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

        public IProduct GetProductById(Guid id)
        {
            IProduct product = null;
            string sql = "SELECT * FROM " + SQLDbHelper.ProductTable + " WHERE Id = @Id";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("Id", id);

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

        public bool DeleteProductById(Guid Id)
        {
            string sql = "DELETE FROM " + SQLDbHelper.ProductTable + " WHERE Id = @Id";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("Id", Id);

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

        public bool UpdateProduct(IProduct product)
        {
            string sql = "UPDATE " + SQLDbHelper.ProductTable
                            + " SET Name = @name, MSRP = @msrp, Description = @description, CUP = @cup "
                            + " WHERE Id = @id";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("Id", product.Id);
                    command.Parameters.AddWithValue("Name", product.Name);
                    command.Parameters.AddWithValue("MSRP", product.MSRP);
                    command.Parameters.AddWithValue("Description", product.Description);
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

        public bool InsertProduct(IProduct product)
        {
            string sql = "INSERT INTO " + SQLDbHelper.ProductTable + " (Id,Name,MSRP,Description,CreationDate,CUP) "
                           + " VALUES (@Id,@Name,@MSRP,@Description,@CreationDate,@CUP)";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("Id", product.Id);
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

        public bool SaveProduct(IProduct product)
        {
            if (GetProductById(product.Id) == null)
            {
                return InsertProduct(product);
            }
            else
            {
                return UpdateProduct(product);
            }
        }

        public IEnumerable<IProduct> GetProductsByCategory(ICategory category)
        {
            List<IProduct> products = new List<IProduct>();
            string sql = "SELECT * FROM " + SQLDbHelper.ProductTable + " WHERE CategoryId = @categoryId";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("categoryId", category.Id);

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

        private Product ParseProduct(SqlDataReader dr)
        {
            try
            {
                return new Product()
                {
                    Id = SQLDbHelper.GetGuid(dr, "Id"),
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