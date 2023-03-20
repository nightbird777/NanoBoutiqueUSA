using System.Data.SqlClient;
using Dapper;

namespace NanoBoutiqueUSA.Models
{
    public class UserDB
    {
        //private string connectionString = ("server=KHALIFABUILD202; database=NanoBoutique; user id=raju; password=raju123");
        private string connectionString = ("server=nyctotampa; database=NanoBoutique; user id=raju; password=raju123");
        public List<User> getAllUsers()
        {
            List<User> users = new List<User>();
            string sql = "select * from [User]";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                users = conn.Query<User>(sql).ToList();
            }
            return users;
        }

        public User getUserById(int id)
        {
            User user = new User();
            string sql = "select * from [User] where Id = " + id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                user = conn.QueryFirst<User>(sql, new { id });
            }
            return user;
        }

        public void saveEditUser(User user)
        {
            string sql = "update [User] set Name = '" + user.Name + "', Phone = '" + user.Phone + "', Email = '" + user.Email + "', " +
                "AddressLine = '" + user.AddressLine + "', City = '" + user.City + "', State = '" + user.State + "', " +
                "Zip = '" + user.Zip + "', Password = '" + user.Password + "', UpdatedBy = '" + user.UpdatedBy + "', " +
                "DateLastUpdated = GETDATE(), Active = '" + user.Active + "' where Id = " + user.Id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, user);
            }
        }

        public void removeUser(int id)
        {
            User user = new User();
            string sql = "delete from [User] where Id = " + id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, new { id });
            }
        }

        public void CreateNewUser(User user)
        {
            string sql = "insert into [User] (Name, Phone, Email, AddressLine, City, State, Zip, Password, CreatedBy, DateCreated, Active) " +
                "values ('" + user.Name + "', '" + user.Phone + "', '" + user.Email + "', '" + user.AddressLine + "', '" + user.City + "', " +
                "'" + user.State + "', '" + user.Zip + "', '" + user.Password + "', '" + user.CreatedBy + "', GETDATE(), '" + user.Active + "')";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, user);
            }
        }
    }
}
