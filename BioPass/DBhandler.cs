using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace BioPass
{
    public class DBhandler
    {
        SQLiteConnection dbConn;
        // Creates an empty database file
        public void createNewDatabase()
        {
            SQLiteConnection.CreateFile("UsrInfo.sqlite");
            this.connectToDatabase();
            this.createTable();
        }

        // Creates a connection with our database file.
        public void connectToDatabase()
        {
            dbConn = new SQLiteConnection("Data Source=UsrInfo.sqlite;Version=3;");
            dbConn.Open();

        }

        // Creates a table named 'Accounts' with two columns: name (a string of max 20 characters) and score (an int)
        public void createTable()
        {
            string sql = "CREATE TABLE Accounts(ActID INTEGER PRIMARY KEY,ActName varchar(255) NOT NULL,Username varchar(255) NOT NULL,Password varchar(255) NOT NULL,PersonId varchar(255) NOT NULL); ";
            SQLiteCommand cmd = new SQLiteCommand(sql, dbConn);
            cmd.ExecuteNonQuery();
        }
        public void addAccount(String act, String usr, String pw, String person_id)
        {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "INSERT INTO Accounts(ActName,Username,Password,PersonId) VALUES(@act,@usr,@pw,@pid)";
            SQLiteParameter account = new SQLiteParameter("@act", act);
            SQLiteParameter userName = new SQLiteParameter("@usr", usr);
            SQLiteParameter password = new SQLiteParameter("@pw", pw);
            SQLiteParameter pid = new SQLiteParameter("@pid", person_id);

            cmd.Parameters.Add(account);
            cmd.Parameters.Add(userName);
            cmd.Parameters.Add(password);
            cmd.Parameters.Add(pid);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

        //test sqlite data
        public void debugSqlite()
        {
            string sql = "SELECT * from Accounts";
            SQLiteCommand command = new SQLiteCommand(sql, dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            Console.WriteLine("List of all accounts in db:");
            while (reader.Read())
                Console.WriteLine("Account: " + reader["ActName"] + "\tUsername: " + reader["Username"]);
            Console.ReadLine();
            /*            Console.WriteLine("This is the info for facepass");
            Console.WriteLine("Username = " + getUsername("facepass"));
            Console.WriteLine("Password = " + getPassword("facepass"));
             */
        }
        public String getUsername(String app, String person_id)
        {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT Username FROM Accounts WHERE ActName LIKE @act AND PersonId=@pid";
            SQLiteParameter account = new SQLiteParameter("@act", app);
            SQLiteParameter person = new SQLiteParameter("@pid", person_id);

            SQLiteDataReader reader = cmd.ExecuteReader();
            String username = "";
            username = (String)(reader["Username"] != System.DBNull.Value ? reader["Username"] : "");
            return username;
        }
        public String getPassword(String app, String person_id)
        {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT Password FROM Accounts WHERE ActName LIKE @act AND PersonId=@pid";

            SQLiteParameter account = new SQLiteParameter("@act", app);
            SQLiteParameter person = new SQLiteParameter("@pid", person_id);

            SQLiteDataReader reader = cmd.ExecuteReader();
            String password = (String)(reader["Password"] != System.DBNull.Value ? reader["Password"] : "");

            return password;
        }

        public Boolean appExists(String app)
        {
            String sql = "SELECT ActName FROM Accounts WHERE ActName='" + app + "'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            String act = (String)(reader["ActName"] != System.DBNull.Value ? reader["ActName"] : "");
            return (act.Length > 0);
        }

        public Boolean exists()
        {
            String path = Environment.CurrentDirectory;
            System.Diagnostics.Debug.WriteLine(path);
            return (System.IO.File.Exists(path + "/UsrInfo.sqlite"));
        }
    }
}