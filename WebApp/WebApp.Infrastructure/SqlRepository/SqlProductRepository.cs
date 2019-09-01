using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using WebApp.BL;
using WebApp.BL.Interface;

namespace WebApp.Infrastructure.SqlRepository
{
    public class SqlProductRepository : IProductRepository
    {
        public List<IProduct> GetAllProducts()
        {
            List<IProduct> products = new List<IProduct>();
            string sql = "SELECT * FROM " + SqlDbHelper.ProductTable;

            using (var con = new SqlConnection())
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
            string sql = "SELECT * FROM " + SqlDbHelper.ProductTable + " WHERE Id = @Id";

            using (var con = new SqlConnection())
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
            string sql = "DELETE FROM " + SqlDbHelper.ProductTable + " WHERE Id = @Id";

            using (var con = new SqlConnection())
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
            string sql = "UPDATE " + SqlDbHelper.ProductTable
                            + " SET Name = @name, MSRP = @msrp, Description = @description, CUP = @cup "
                            + " WHERE Id = @id";

            using (var con = new SqlConnection())
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
            string sql = "INSERT INTO " + SqlDbHelper.ProductTable + " (Id,Name,MSRP,Description,CreationDate,CUP) "
                           + " VALUES (@Id,@Name,@MSRP,@Description,@CreationDate,@CUP)";

            using (var con = new SqlConnection())
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
            string sql = "SELECT * FROM " + SqlDbHelper.ProductTable + " WHERE CategoryId = @categoryId";

            using (var con = new SqlConnection())
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
                    Id = dr.GetGuid("Id"),
                    Name = dr.GetValueOrDefault("Name", ""),
                    MSRP = dr.GetDouble("MSRP"),
                    CreationDate = dr.GetDateTime("CreationDate"),
                    CUP = dr.GetValueOrDefault("CUP", ""),
                    Description = dr.GetValueOrDefault("Description", "")
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}