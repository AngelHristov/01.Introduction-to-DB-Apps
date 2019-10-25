namespace _01.InitialSetup
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        private static string connectionString =
            $"Server = JON-PC\\SQLEXPRESS;" +
            " Database ={0};" +
            " Integrated Security = true";

        public static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(String.Format(connectionString, "master"));

            
            try
            {
                connection.Open();

                using (connection)
                {
                    SqlCommand command = new SqlCommand("CREATE DATABASE MinionsDB", connection);

                    command.ExecuteNonQuery();

                    Console.WriteLine("Database MinionsDB created successfully!");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Something gone wrong with creating MinionsDB Database!");
                Console.WriteLine($"{e.Message}");
            }

            // connection = new SqlConnection(String.Format(connectionString, "MinionsDB"));
        }
    }
}
