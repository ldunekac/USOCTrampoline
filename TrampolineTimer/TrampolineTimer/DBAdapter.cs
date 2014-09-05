using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TrampolineTimer.Data;
using System.Data.SQLite;

namespace TrampolineTimer
{
    /**
     * Singleton to access the database.
     */
    class DBAdapter
    {
        private static string SQLConnectionString = "Data Source=training.db; Version=3;";

        private static string CreateAthletesTableString = @"CREATE TABLE IF NOT EXISTS Athletes(Id integer primary key, firstName varchar(255), lastName varchar(255), age varchar(255), height varchar(255), weight varchar(255))";
        private static string SelectAllFromAthletesTableString = @"SELECT * FROM Athletes";

        private static string CreateCoachesTableString = @"CREATE TABLE IF NOT EXISTS Coaches(Id integer primary key, firstName varchar(255), lastName varchar(255))";

        private static string SelectAllFromCoachesTableString = @"SELECT * FROM Coaches";

        private static string CreateGymTableString = "CREATE TABLE IF NOT EXISTS Gym(Id integer primary key, gymName varchar(255), location varchar(255))";
        private static string InsertGymTableString = "INSERT INTO Gym(Id, gymName, location) VALUES (@Id, @gymName, @location)";
        private static string SelectGymTableString = "SELECT {0} FROM Gym";

        private static string CreateSessionsTableString = "CREATE TABLE IF NOT EXISTS Sessions(Id integer primary key, athleteId integer, coachId integer, startTime datetime, videoFilename string);";
        private static string CreateSessionJumpsTableString = "CREATE TABLE IF NOT EXISTS SessionJumps(sessionId integer, start double, length double);";
        private static DBAdapter instance;

        private DBAdapter()
        {
            CreateTables();
        }

        private SQLiteConnection GetConnection() {
            var conn = new SQLiteConnection(SQLConnectionString);
            conn.Open();

            return conn;
        }

        public static DBAdapter Instance
        {
            get { if (instance == null) instance = new DBAdapter(); return instance; }
        }

        // Set up
        public String GymName
        {
            get { return SelectFromGymTable("gymName"); }
        }

        public String GymLocation
        {
            get { return SelectFromGymTable("location"); }
        }

        public List<Athlete> Athletes
        {
            get { return SelectFromAthletesTable(); }
        }

        public List<Coach> Coaches
        {
            get { return SelectFromCoachesTable(); }
        }

        public void CreateTables()
        {
            CreateAthletesTable();
            CreateCoachesTable();
            CreateGymTable();
            CreateSessionsTable();
            CreateSessionJumpsTable();
        }

        // Athletes Table methods
        private void CreateAthletesTable()
        {
            using (var conn = GetConnection()) // Will automatically close the connection
            {
                SQLiteCommand CreateTableCommand = conn.CreateCommand();
                CreateTableCommand.CommandText = CreateAthletesTableString;
                CreateTableCommand.ExecuteNonQuery();
            }
        }

        public void InsertIntoAthletesTable(List<Athlete> athletes)
        {
            using (var conn = GetConnection())
            {
                SQLiteCommand InsertTableCommand = conn.CreateCommand();
                InsertTableCommand.CommandText = @"INSERT INTO Athletes(firstName, lastName, age, height, weight) VALUES(@firstName, @lastName, @age, @height, @weight)";
                

                foreach (var athlete in athletes)
                {
                    InsertTableCommand.Parameters.Clear();

                    InsertTableCommand.Parameters.AddWithValue("@firstName", athlete.FirstName);
                    InsertTableCommand.Parameters.AddWithValue("@lastName", athlete.LastName);
                    InsertTableCommand.Parameters.AddWithValue("@age", athlete.Age);
                    InsertTableCommand.Parameters.AddWithValue("@height", athlete.Height);
                    InsertTableCommand.Parameters.AddWithValue("@weight", athlete.Weight);

                    InsertTableCommand.ExecuteNonQuery();
                    athlete.Id = conn.LastInsertRowId;
                }
            }
        }

        private List<Athlete> SelectFromAthletesTable()
        {
            List<Athlete> athletes = new List<Athlete>();

            using (var conn = GetConnection())
            {
                SQLiteCommand SelectTableCommand = conn.CreateCommand();
                SelectTableCommand.CommandText = SelectAllFromAthletesTableString;

                SQLiteDataReader reader = SelectTableCommand.ExecuteReader();
                while (reader.Read())
                {
                    athletes.Add(new Athlete
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            Age = reader[3].ToString(),
                            Height = reader[4].ToString(),
                            Weight = reader[5].ToString(),
                        });
                }

                return athletes;
            }
        }

        // Coaches Table methods
        private void CreateCoachesTable()
        {

            using (var conn = GetConnection())
            {
                SQLiteCommand CreateTableCommand = conn.CreateCommand();
                CreateTableCommand.CommandText = CreateCoachesTableString;
                CreateTableCommand.ExecuteNonQuery();
            }
        }

        public void InsertIntoCoachesTable(List<Coach> coaches)
        {

            using (var conn = GetConnection())
            {
                foreach (var coach in coaches)
                {
                    SQLiteCommand InsertTableCommand = conn.CreateCommand();
                    InsertTableCommand.CommandText = @"INSERT INTO Coaches(firstName, lastName)
                                                                        VALUES('" + coach.FirstName + "', '" + coach.LastName + "')";
                    InsertTableCommand.ExecuteNonQuery();
                }
            }
        }

        private List<Coach> SelectFromCoachesTable()
        {
            List<Coach> coaches = new List<Coach>();

            using (var conn = GetConnection())
            {
                SQLiteCommand SelectTableCommand = conn.CreateCommand();
                SelectTableCommand.CommandText = SelectAllFromCoachesTableString;

                SQLiteDataReader reader = SelectTableCommand.ExecuteReader();
                while (reader.Read())
                {
                    coaches.Add(new Coach
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader[1].ToString(),
                        LastName = reader[2].ToString(),
                    });
                }

                return coaches;
            }
        }

        // Gym Table methods
        private void CreateGymTable()
        {

            using (var conn = GetConnection())
            {
                SQLiteCommand CreateTableCommand = conn.CreateCommand();
                CreateTableCommand.CommandText = CreateGymTableString;
                CreateTableCommand.ExecuteNonQuery();
            }
        }

        public void InsertIntoGymTable(String name, String location)
        {

            using (var conn = GetConnection())
            {
                SQLiteCommand InsertTableCommand = conn.CreateCommand();
                InsertTableCommand.CommandText = InsertGymTableString;
                InsertTableCommand.Parameters.AddWithValue("@Id", Convert.ToInt32(1));
                InsertTableCommand.Parameters.AddWithValue("@gymName", name);
                InsertTableCommand.Parameters.AddWithValue("@location", location);
                InsertTableCommand.ExecuteNonQuery();
            }
        }

        private String SelectFromGymTable(String column)
        {

            using (var conn = GetConnection())
            {
                SQLiteCommand SelectTableCommand = conn.CreateCommand();
                SelectTableCommand.CommandText = String.Format(SelectGymTableString, column);
                SQLiteDataReader reader = SelectTableCommand.ExecuteReader();
                while (reader.Read())
                {
                    // This is the hack to break all hacks.
                    // If you want avoid pulling a single gym,
                    // too bad. We're too lazy to fix this right now.
                    return reader[0].ToString();
                }
                return null;
            }
        }

        // Session methods
        private void CreateSessionsTable()
        {
            using (var conn = GetConnection())
            {
                SQLiteCommand CreateTableCommand = conn.CreateCommand();
                CreateTableCommand.CommandText = CreateSessionsTableString;
                CreateTableCommand.ExecuteNonQuery();
            }
        }
        
        private void CreateSessionJumpsTable()
        {
            using (var conn = GetConnection())
            {
                SQLiteCommand CreateTableCommand = conn.CreateCommand();
                CreateTableCommand.CommandText = CreateSessionJumpsTableString;
                CreateTableCommand.ExecuteNonQuery();
            }
        }

        public void SaveSession(Session session)
        {
            using (var conn = GetConnection())
            {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Sessions(athleteId, coachId, startTime, videoFilename) VALUES(@athleteId, @coachId, @startTime, @videoFilename);";
                cmd.Parameters.AddWithValue("@athleteId", session.Athlete.Id);
                cmd.Parameters.AddWithValue("@coachId", session.Coach.Id);
                cmd.Parameters.AddWithValue("@startTime", session.StartTime);
                cmd.Parameters.AddWithValue("@videoFilename", session.VideoFilename);
                cmd.ExecuteNonQuery();

                long sessionId = conn.LastInsertRowId;
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO SessionJumps(sessionId, start, length) VALUES(@sessionId, @start, @length);";

                foreach (var jump in session.Jumps)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@sessionId", sessionId);
                    cmd.Parameters.AddWithValue("@start", (jump.StartTime - session.StartTime).TotalSeconds);
                    cmd.Parameters.AddWithValue("@length", jump.Length);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
