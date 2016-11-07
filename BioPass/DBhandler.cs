﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data;

namespace BioPass
{
    public class DBhandler
    {
        SQLiteConnection dbConn;
        // Creates an empty database file
        public void createNewDatabase()
        {
            SQLiteConnection.CreateFile("biopass.sqlite");
            this.connectToDatabase();
            this.createTables();
        }

        // Creates a connection with our database file.
        public void connectToDatabase()
        {
            dbConn = new SQLiteConnection("Data Source=biopass.sqlite;Version=3;");
            dbConn.Open();

        }

        public void createTables()
        {
            string sql = @"
CREATE TABLE userAccount(
account_id INTEGER PRIMARY KEY,
username varchar(255) NOT NULL,
password varchar(255) NOT NULL,
application_id varchar(255) NOT NULL,
user_id varchar(255) NOT NULL);
CREATE TABLE application(
application_id INTEGER PRIMARY KEY,
name varchar(255) NOT NULL,
type INTEGER NOT NULL,
username_field varchar(255) NOT NULL,
password_field varchar(255) NOT NULL);
CREATE TABLE user(
user_id INTEGER PRIMARY KEY,
name varchar(255) NOT NULL,
fingerprint BLOB,
pin INTEGER);";
            SQLiteCommand cmd = new SQLiteCommand(sql, dbConn);
            cmd.ExecuteNonQuery();
        }

        //test sqlite data
        public void debugSqlite()
        {
            string sql = "SELECT * from userAccount";
            SQLiteCommand command = new SQLiteCommand(sql, dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            Console.WriteLine("List of all accounts in db:");
            while (reader.Read())
                Console.WriteLine("account: " + reader["application_id"] + "\tusername: " + reader["username"]);
            Console.ReadLine();
            /*            Console.WriteLine("This is the info for facepass");
            Console.WriteLine("Username = " + getUsername("facepass"));
            Console.WriteLine("Password = " + getPassword("facepass"));
             */
        }

        // userAccount table
        public void addAccount(String usr, String pw, String aid, String uid)
        {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "INSERT INTO userAccount(username,password,application_id,user_id) VALUES(@usr,@pw,@aid,@uid)";

            cmd.Parameters.Add(new SQLiteParameter("@usr", usr));
            cmd.Parameters.Add(new SQLiteParameter("@pw", pw));
            cmd.Parameters.Add(new SQLiteParameter("@aid", aid));
            cmd.Parameters.Add(new SQLiteParameter("@uid", uid));

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public String getUsername(String aid, String uid)
        {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT username FROM userAccount WHERE application_id LIKE @aid AND user_id=@uid";
            cmd.Parameters.Add(new SQLiteParameter("@aid", aid));
            cmd.Parameters.Add(new SQLiteParameter("@pid", uid));
            cmd.Prepare();

            SQLiteDataReader reader = cmd.ExecuteReader();
            String username = "";
            username = (String)(reader["username"] != System.DBNull.Value ? reader["username"] : "");
            return username;
        }
        public String getPassword(String aid, String uid)
        {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT password FROM userAccount WHERE application_id LIKE @aid AND user_id=@uid";

            cmd.Parameters.Add(new SQLiteParameter("@aid", aid));
            cmd.Parameters.Add(new SQLiteParameter("@uid", uid));
            cmd.Prepare();

            SQLiteDataReader reader = cmd.ExecuteReader();
            String password = (String)(reader["password"] != System.DBNull.Value ? reader["password"] : "");

            return password;
        }

        public Boolean appExists(String aid)
        {
            String sql = "SELECT application_id FROM userAccount WHERE application_id='" + aid + "'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            String act = (String)(reader["application_id"] != System.DBNull.Value ? reader["application_id"] : "");
            return (act.Length > 0);
        }

        // User table

         public long addUser(String name) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "INSERT INTO user(name) VALUES(@name)";

            cmd.Parameters.Add(new SQLiteParameter("@name", name));

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd.CommandText = "select last_insert_rowid();";
            SQLiteDataReader reader = cmd.ExecuteReader();
            long row = (long) reader[0];

            return row;

        }
        public String getUserName(long uid) {
            String sql = "SELECT name FROM user WHERE user_id='" + uid + "'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            String name = (String)(reader["name"] != System.DBNull.Value ? reader["name"] : "");
            return name;
        }
        public void registerUserFP(long uid, String fp) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "UPDATE user SET fingerprint=@fp WHERE user_id=@uid;";

            cmd.Parameters.Add(new SQLiteParameter("@fp", fp));
            cmd.Parameters.Add(new SQLiteParameter("@uid", uid));

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public DataTable getAllUsersFP() {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT user_id, fingerprint FROM user;";

            cmd.Prepare();
            SQLiteDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            return dt;
        }

        public Boolean exists()
        {
            String path = Environment.CurrentDirectory;
            System.Diagnostics.Debug.WriteLine(path);
            return (System.IO.File.Exists(path + "/biopass.sqlite"));
        }
    }
}