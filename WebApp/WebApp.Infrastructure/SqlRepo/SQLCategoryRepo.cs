using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using WebApp.BL;
using WebApp.BL.Interface;

namespace WebApp.Infrastructure.SqlRepo
{
    public class SqlCategoryRepo : ICategoryRepo
    {
        private List<ICategory> GetAllCategories()
        {
            List<ICategory> categories = new List<ICategory>();
            string sql = "SELECT * FROM " + SqlDbHelper.CategoryTable;

            using (var con = new SqlConnection())
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

        public bool DeleteCategoryById(Guid id)
        {
            string sql = "DELETE FROM " + SqlDbHelper.CategoryTable + " WHERE Id = @id";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("id", id);

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

        public bool UpdateCategory(ICategory category)
        {
            bool isMainCategory = false;


            string sql = "UPDATE " + SqlDbHelper.CategoryTable
                            + " SET Name = @name, Description = @description"
                            + ((isMainCategory = category.ParentCategory == null || category.ParentCategory.Id == Guid.Empty)
                            ? " WHERE Id = @id"
                            : ", ParentId = @parentId WHERE Id = @id");

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("id", category.Id);
                    command.Parameters.AddWithValue("name", category.Name);
                    command.Parameters.AddWithValue("description", category.Description);

                    if (!isMainCategory)
                    {
                        command.Parameters.AddWithValue("parentId", (category.ParentCategory == null)
                                                        ? Guid.Empty : category.ParentCategory?.Id ?? Guid.Empty);
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

        public ICategory GetCategoryById(Guid id)
        {
            ICategory category = null;
            string sql = "SELECT * FROM " + SqlDbHelper.CategoryTable + " WHERE Id = @Id";

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
                        category = ParseCategory(dr);
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

            return category;
        }

        public List<ICategory> GetSubCategoriesById(Guid id)
        {
            List<ICategory> categories = new List<ICategory>();
            string sql = "SELECT * FROM " + SqlDbHelper.CategoryTable + " WHERE ParentId = @parentId";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("parentId", id);

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
            if (GetCategoryById(category.Id) == null)
            {
                return InsertCategory(category);
            }
            else
            {
                return UpdateCategory(category);
            }
        }

        public bool InsertCategory(ICategory category)
        {
            bool isMainCategory = false;


            string sql = "INSERT INTO " + SqlDbHelper.CategoryTable + " (Id,Name,Description"
                            + ((isMainCategory = category.ParentCategory == null || category.ParentCategory.Id == Guid.Empty)
                            ? ")  VALUES (@Id,@name,@description)"
                            : ",ParentId) VALUES (@Id,@name,@description, @parentId)");

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("Id", category.Id);
                    command.Parameters.AddWithValue("name", category.Name);
                    command.Parameters.AddWithValue("description", category.Description);

                    if (!isMainCategory)
                    {
                        command.Parameters.AddWithValue("parentId", (category.ParentCategory == null)
                                                        ? Guid.Empty : category.ParentCategory?.Id ?? Guid.Empty);
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
                var Id = SqlDbHelper.GetGuid(dr, "Id");
                var name = SqlDbHelper.GetValueOrDefault(dr, "Name", "");
                var parentCategoryId = SqlDbHelper.GetGuid(dr, "ParentId");
                Category parentCategory = null;
                if (parentCategoryId != Guid.Empty)
                    parentCategory = (Category)GetCategoryById(parentCategoryId);
                var description = SqlDbHelper.GetValueOrDefault(dr, "Description", "");

                var subCategories = GetSubCategoriesById(Id);

                return new Category()
                {
                    Id = Id,
                    Name = name,
                    ParentCategory = parentCategory,
                    Description = description,
                    SubCategories = subCategories
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}