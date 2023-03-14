using System.Data.SqlClient;

namespace NanoBoutiqueUSA.Models
{
    public class LoginDB
    {
        public Login signIn(string email, string password)
        {
            string connectionString = ("server=KHALIFABUILD202; database=NanoBoutique; user id=raju; password=raju123");
            //string connectionString = ("server=nyctotampa; database=NanoBoutique; user id=raju; password=raju123");
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "select * from loginID where email = '" + email
                            + "' and password = '" + password + "'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Login login = new Login();
            while (reader.Read())
            {
                login.UserId = Convert.ToInt32(reader["UserId"]);
                login.Email = reader["Email"].ToString();
                login.Password = reader["Password"].ToString();
            }
            reader.Close();
            conn.Close();
            return login;
        }
    }
}
