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

        private static string dataBaseName = "MinionsDB";

        public static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(String.Format(connectionString, "master"));
            
            try
            {
                connection.Open();

                using (connection)
                {
                    SqlCommand command = new SqlCommand($"CREATE DATABASE {dataBaseName}", connection);

                    command.ExecuteNonQuery();

                    Console.WriteLine($"Database {dataBaseName} created successfully!");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine($"Something gone wrong with creating {dataBaseName} Database!");
                Console.WriteLine($"{e.Message}");
            }

            connection = new SqlConnection(String.Format(connectionString, dataBaseName));

            connection.Open();

            using (connection)
            {
                string queryText = @"CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))

                                     CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))

                                     CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))

                                    CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))

                                    CREATE TABLE Villains (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))

                                    CREATE TABLE MinionsVillains (MinionId INT FOREIGN KEY REFERENCES Minions(Id),VillainId INT FOREIGN KEY REFERENCES Villains(Id),CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId))";
               
                SqlCommand command = new SqlCommand(queryText, connection);

                command.ExecuteNonQuery();
            }
        }
    }
}
