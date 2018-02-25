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
    public class SQLUserRepo : SQLDbConnection, IUserRepo
    {
        public List<IUser> GetAllUsers()
        {
            List<IUser> users = new List<IUser>();
            string sql = "SELECT * FROM " + SQLDbHelper.UserTable;

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);

                    con.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        users.Add(ParseUser(dr));
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

            return users;
        }

        public List<IUser> Users => GetAllUsers();

        public bool DeleteUserByID(Guid ID)
        {
            string sql = "DELETE FROM " + SQLDbHelper.UserTable + " WHERE ID = @ID";

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

        public bool SaveUser(IUser user)
        {
            string sql = "INSERT INTO " + SQLDbHelper.UserTable + " (ID,UserName,Password,Email,CreationDate,CartID,Address) "
                            + " VALUES (@ID,@userName,@password,@email,@creationDate,@cartID,@address)";

            using (var con = Connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sql, con);
                    command.Parameters.AddWithValue("ID", user.ID);
                    command.Parameters.AddWithValue("userName", user.UserName);
                    command.Parameters.AddWithValue("password", user.Password);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("creationDate", user.CreationDate);
                    command.Parameters.AddWithValue("cartID", "TODO");
                    command.Parameters.AddWithValue("address", user.Address);

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

        private User ParseUser(SqlDataReader dr)
        {
            try
            {
                return new User()
                {
                    ID = SQLDbHelper.GetGuid(dr, "ID"),
                    UserName = SQLDbHelper.GetValueOrDefault(dr, "UserName", ""),
                    Password = SQLDbHelper.GetValueOrDefault(dr, "Password", ""),
                    Address = SQLDbHelper.GetValueOrDefault(dr, "Address", ""),
                    Email = SQLDbHelper.GetValueOrDefault(dr, "Email", ""),
                    CreationDate = SQLDbHelper.GetDateTime(dr, "CreationDate")
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
