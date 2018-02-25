using SGEGService.Model.Interface;
using SGEGService.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Repository.SQLRepo
{
    public class SQLCategoryRepo : SQLDbConnection, ICategoryRepo
    {
        private List<ICategory> GetAllCategories()
        {
            List<ICategory> categories = new List<ICategory>();
            string sql = "SELECT * FROM " + SQLDbHelper.CategoryTable;

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);

                    con.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        categories.Add(ParseCategory(dr));
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

            return categories;
        }

        public List<ICategory> Caterogies => GetAllCategories();

        public bool DeleteCategoryByID(Guid id)
        {
            string sql = "DELETE FROM " + SQLDbHelper.CategoryTable + " WHERE ID = @ID";

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

        public List<ICategory> GetSubCategoriesByID(Guid id)
        {
            List<ICategory> categories = new List<ICategory>();
            string sql = "SELECT * FROM " + SQLDbHelper.CategoryTable + " WHERE ParentID = @parentID";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("parentID", id);

                    con.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        categories.Add(ParseCategory(dr));
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

            return categories;
        }

        public bool SaveCategory(ICategory category)
        {
            bool isMainCategory = false;


            string sql = "INSERT INTO " + SQLDbHelper.CategoryTable + " (ID,Name,Description"
                            + ((isMainCategory = category.ParentCategory == null || category.ParentCategory.ID == Guid.Empty)
                            ? ")  VALUES (@ID,@name,@description)"
                            : ",ParentID) VALUES (@ID,@name,@description, @parentID)");

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("ID", category.ID);
                    command.Parameters.AddWithValue("name", category.Name);
                    command.Parameters.AddWithValue("description", category.Description);

                    if (!isMainCategory)
                    {
                        command.Parameters.AddWithValue("parentID", (category.ParentCategory == null)
                                                        ? Guid.Empty : category.ParentCategory?.ID ?? Guid.Empty);
                    }

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

        public Category ParseCategory(SqlDataReader dr)
        {
            try
            {
                var ID = SQLDbHelper.GetGuid(dr, "ID");
                var name = SQLDbHelper.GetValueOrDefault(dr, "Name", "");
                //var parentCategory = Caterogies.First(c => c.ID == SQLDbHelper.GetGuid(dr, "ParentID"));
                var description = SQLDbHelper.GetValueOrDefault(dr, "Description", "");
                //var subCategories = GetSubCategoriesByID(ID);

                return new Category()
                {
                    ID = ID,
                    Name = name,
                    //ParentCategory = parentCategory,
                    Description = description,
                    //SubCategories = subCategories
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
