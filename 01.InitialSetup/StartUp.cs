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

        public static void Main()
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
                Console.WriteLine(e.Message);
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

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Creating tables done!");
                }
                catch (Exception e)
                {

                    Console.WriteLine("Creation of tables didn't happen!");
                    Console.WriteLine(e.Message);
                }

                queryText = @"INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')

                                INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)

                                INSERT INTO Minions (Name,Age, TownId) VALUES('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)

                                INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')

                                INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)

                                INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7)";

                SqlCommand insertCmd = new SqlCommand(queryText, connection);

                try
                {
                    int rowsAffected = insertCmd.ExecuteNonQuery();
                    Console.WriteLine($"Inserted data into {dataBaseName} done! {rowsAffected} rows was affected!");
                }
                catch (Exception e)
                {

                    Console.WriteLine($"Insert of data into {dataBaseName} failed!");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
