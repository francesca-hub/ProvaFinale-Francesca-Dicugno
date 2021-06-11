using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Prova_finale
{
    class DbManagerConnectedmode
    {
        const string connectionString = @"Data Source= (localdb)\MSSQLLocalDB;" +
                                          "Initial Catalog = Prova;" +
                                          "Integrated Security=true;";
        public static void RegisterUser(string username)
        {
            //Se manca la connessione ad Db scateno una sql Exception
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;



                    command.CommandText = "insert into dbo.Utente values(@Username)";
                    command.Parameters.AddWithValue("@Username", username);


                    command.ExecuteNonQuery();

                    connection.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        internal static bool GetUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))

            {

                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;

                command.CommandText = "select * from dbo.Utente where Username = @Username";
                command.Parameters.AddWithValue("@Username", username);

                SqlDataReader reader = command.ExecuteReader();

                return reader.HasRows; //Se true, non devo poter aggiungere l'utente

                connection.Close();
           
            }
        }
    }
}