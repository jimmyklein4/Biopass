﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BioPass
{
    public class DBhandler
    {
        public SQLiteConnection dbConn;
        // Creates an empty database file
        public void createNewDatabase()
        {
            SQLiteConnection.CreateFile("biopass.sqlite");
            this.connectToDatabase();
            this.createTables();
            this.prepUserDB();
            //this.prepAppDB();
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
password BLOB NOT NULL,
application_id varchar(255) NOT NULL,
user_id INTEGER NOT NULL);" +
                @"
CREATE TABLE application(
application_id INTEGER PRIMARY KEY,
name varchar(255) NOT NULL,
type varchar(255) NOT NULL,
username_field varchar(255),
password_field varchar(255),
submit_btn varchar(255),
login_page varchar(255));" +
                @"
CREATE TABLE user(
user_id INTEGER PRIMARY KEY,
name varchar(255) NOT NULL,
pin varchar(255),
security_level INTEGER);" +
                @"
CREATE TABLE fingerprint(
fp_id INTEGER PRIMARY KEY,
finger varchar(255) NOT NULL,
fingerprint BLOB,
user_id INTEGER NOT NULL);" +
                @"
CREATE TABLE iris(
iris_id INTEGER PRIMARY KEY,
iris_data varchar(255) NOT NULL,
user_id INTEGER NOT NULL);";
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
            byte[] passwordBlob = DPAPI.protect(pw);
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "INSERT INTO userAccount(username,password,application_id,user_id) VALUES(@usr,@pw,@aid,@uid);";

            cmd.Parameters.Add(new SQLiteParameter("@usr", usr));
            cmd.Parameters.Add(new SQLiteParameter("@pw", passwordBlob));
            cmd.Parameters.Add(new SQLiteParameter("@aid", aid));
            cmd.Parameters.Add(new SQLiteParameter("@uid", uid));

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public String getUsername(String aid) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT username FROM userAccount WHERE account_id=@aid;";
            cmd.Parameters.Add(new SQLiteParameter("@aid", aid));
            cmd.Prepare();
            String username = "";

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    username = (String)(reader["username"] != System.DBNull.Value ? reader["username"] : "");
                }
            }
            return username;
        }

        public String getPassword(String aid)
        {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT password FROM userAccount WHERE account_id=@aid";

            cmd.Parameters.Add(new SQLiteParameter("@aid", aid));
            cmd.Prepare();
            byte[] passwordBlob;
            String password = "";

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    passwordBlob = (byte[])(reader["password"] != System.DBNull.Value ? reader["password"] : "");
                    password = DPAPI.unprotect(passwordBlob);
                }
            }

            return password;
        }
        public void updateUserCreds(String aid, String username, String password) {
            byte[] passwordBlob = DPAPI.protect(password);

            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "UPDATE userAccount SET username=@un, password=@pw WHERE account_id=@aid;";

            cmd.Parameters.Add(new SQLiteParameter("@un", username));
            cmd.Parameters.Add(new SQLiteParameter("@pw", passwordBlob));
            cmd.Parameters.Add(new SQLiteParameter("@aid", aid));

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public DataTable getUserCredentials(long uid) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT userAccount.account_id, application.application_id, application.name, application.type, userAccount.username, userAccount.password FROM userAccount,application " + 
                @"WHERE user_id=@uid AND userAccount.application_id=application.application_id;";

            cmd.Parameters.Add(new SQLiteParameter("@uid", uid));

            cmd.Prepare();
            SQLiteDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            return dt;
        }
        public string getAppType(string aid) {
            String sql = "SELECT application.type FROM application,userAccount WHERE userAccount.account_id=@aid AND application.application_id==userAccount.application_id;";
            SQLiteCommand cmd = new SQLiteCommand(sql, dbConn);
            cmd.Parameters.Add(new SQLiteParameter("@aid", aid));
            cmd.Prepare();

            String app_type = "";

            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                app_type = (String)(reader["type"] != System.DBNull.Value ? reader["type"] : "");
            }
            return app_type;
        }
        public long getAppFromUID(String aid, String uid) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT userAccount.account_id FROM application, userAccount WHERE userAccount.user_id=@uid AND application.name=@aid AND userAccount.application_id=application.application_id";

            cmd.Parameters.Add(new SQLiteParameter("@aid", aid));
            cmd.Parameters.Add(new SQLiteParameter("@uid", uid));
            cmd.Prepare();

            SQLiteDataReader reader = cmd.ExecuteReader();
            long account_id = -1;
            if(reader.Read()) {
                account_id = (long)(reader["account_id"] != System.DBNull.Value ? reader["account_id"] : 0);
            }
            return account_id;
        }
        public String appExists(String website) {
            String sql = "SELECT login_page FROM application WHERE name=@web;";
            SQLiteCommand cmd = new SQLiteCommand(sql, dbConn);
            cmd.Parameters.Add(new SQLiteParameter("@web", website));
            cmd.Prepare();

            String login_page = "";

            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                login_page = (String)(reader["login_page"] != System.DBNull.Value ? reader["login_page"] : "");
            }
            return login_page;
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
            reader.Read();

            long row = (long) reader[0];

            return row;
        }

        public long addUser(String name, String pin) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "INSERT INTO user(name, pin) VALUES(@name, @pin)";

            cmd.Parameters.Add(new SQLiteParameter("@name", name));
            cmd.Parameters.Add(new SQLiteParameter("@pin", pin));

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd.CommandText = "select last_insert_rowid();";
            SQLiteDataReader reader = cmd.ExecuteReader();
            reader.Read();

            long row = (long)reader[0];

            return row;
        }
        public String getUserName(long uid) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT name FROM user WHERE user_id='" + uid + "';";
            cmd.Prepare();
            SQLiteDataReader reader = cmd.ExecuteReader();
            reader.Read();

            String name = (String)(reader["name"] != System.DBNull.Value ? reader["name"] : "");
            return name;
        }
        public String compareIdToPin(long uid, String pin) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT name, user_id FROM user WHERE user_id=@uid AND pin=@pin;";
            cmd.Parameters.Add(new SQLiteParameter("@uid", uid));
            cmd.Parameters.Add(new SQLiteParameter("@pin", pin));

            cmd.Prepare();
            SQLiteDataReader reader = cmd.ExecuteReader();
            String user_id = "";
            while(reader.Read()) { 
               user_id = (string)(reader["name"] != System.DBNull.Value ? reader["name"] : "");
            }
            return user_id;
        }
        public int getUserSecurityLevel(long uid) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT security_level FROM user WHERE user_id=@uid;";
            cmd.Parameters.Add(new SQLiteParameter("@uid", uid));

            cmd.Prepare();
            SQLiteDataReader reader = cmd.ExecuteReader();
            int security_level = -1;
            if(reader.Read()) { 
               security_level = (reader["security_level"] != System.DBNull.Value ? ((int)(long) reader["security_level"]) : 0);
            } else {
                security_level = 0;
            }
            return security_level;
        }
        public void updateSecurityLevel(long uid, int security_level) {

            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "UPDATE user SET security_level=@sl WHERE user_id=@uid;";

            cmd.Parameters.Add(new SQLiteParameter("@uid", uid));
            cmd.Parameters.Add(new SQLiteParameter("@sl", security_level));

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public long registerUserFP(long uid, string fp, string fpName) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "INSERT INTO fingerprint(fingerprint, user_id, finger) VALUES (@fp, @uid, @fpName);";

            cmd.Parameters.Add(new SQLiteParameter("@fp", fp));
            cmd.Parameters.Add(new SQLiteParameter("@uid", uid));
            cmd.Parameters.Add(new SQLiteParameter("@fpName", fpName));

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd.CommandText = "select last_insert_rowid();";
            SQLiteDataReader reader = cmd.ExecuteReader();
            reader.Read();

            long row = (long)reader[0];

            return row;
        }

        public void deleteFP(long fp) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "DELETE FROM fingerprint where fp_id=@fp";

            cmd.Parameters.Add(new SQLiteParameter("@fp", fp));

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public string getUserNameByFinger(long fp_id) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT user.name FROM user,fingerprint WHERE fp_id=@fp_id AND user.user_id=fingerprint.user_id;";
            cmd.Parameters.Add(new SQLiteParameter("@fp_id", fp_id));

            cmd.Prepare();
            SQLiteDataReader reader = cmd.ExecuteReader();
            reader.Read();

            String name = (String)(reader["name"] != System.DBNull.Value ? reader["name"] : "");
            return name;
        }

        public string[] getUserArrayByFinger(long fp_id) {
            string[] array = new string[2];
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT user.name, user.user_id FROM user,fingerprint WHERE fp_id=@fp_id AND user.user_id=fingerprint.user_id;";
            cmd.Parameters.Add(new SQLiteParameter("@fp_id", fp_id));

            cmd.Prepare();
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) {

                String name = (String)(reader["name"] != System.DBNull.Value ? reader["name"] : "");
                String user_id = (String)(reader["user_id"] != System.DBNull.Value ? reader["user_id"].ToString() : "");

                array[0] = name;
                array[1] = user_id;
            }
            return array;
        }

        public DataTable getUserFPs(long uid) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT fp_id, fingerprint, finger FROM fingerprint WHERE user_id=@uid;";

            cmd.Parameters.Add(new SQLiteParameter("@uid", uid));

            cmd.Prepare();
            SQLiteDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            return dt;
        }

        public DataTable getAllUsersFP() {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT fp_id, fingerprint FROM fingerprint;";

            cmd.Prepare();
            SQLiteDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            return dt;
        }

        public void prepUserDB() {
            SQLiteCommand cmd = new SQLiteCommand(dbConn);
            using (var transaction = dbConn.BeginTransaction()) {
                // 40 inserts
                for (var i = 1; i < 41; i++) {
                    cmd.CommandText = @"INSERT INTO user (name) VALUES ('fakeuser"+i+@"');";
                    Debug.WriteLine(cmd.CommandText);
                    cmd.ExecuteNonQuery();

                }

                transaction.Commit();
            }
        }
        
        public long addIrisData(String data, long userId) {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "INSERT INTO iris (iris_data, user_id) VALUES (@iid, @uid)";

            cmd.Parameters.Add(new SQLiteParameter("@iid", data));
            cmd.Parameters.Add(new SQLiteParameter("@uid", userId));
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd.CommandText = "select last_insert_rowid();";
            SQLiteDataReader reader = cmd.ExecuteReader();
            reader.Read();

            long row = (long)reader[0];

            return row;
        }

        public Boolean exists() {
            String path = Environment.CurrentDirectory;
            Debug.WriteLine(path);
            return (System.IO.File.Exists(path + "/biopass.sqlite"));
        }

        //application table
        public void addApp(String appname, String type, String usrnameField, String pwField, String submit_btn, String loginPage)
        {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = @"INSERT INTO application(name, type, username_field, password_field, login_page, submit_btn) VALUES (@nm,@tp,@usf,@pwf,@lp,@sbtn);";

            cmd.Parameters.Add(new SQLiteParameter("@nm", appname));
            cmd.Parameters.Add(new SQLiteParameter("@tp", type));
            cmd.Parameters.Add(new SQLiteParameter("@usf", usrnameField));
            cmd.Parameters.Add(new SQLiteParameter("@pwf", pwField));
            cmd.Parameters.Add(new SQLiteParameter("@lp", loginPage));
            cmd.Parameters.Add(new SQLiteParameter("@sbtn", submit_btn));

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public void prepAppDB() {
            SQLiteCommand cmd = new SQLiteCommand(dbConn);
            using (var transaction = dbConn.BeginTransaction()) {
                string[] names = {"facebook.com", "tumail.temple.edu", "learn.temple.edu"};
                string[] types = {"0", "0", "0"};
                string[] ufield = {"#email", "#ctl00_ContentPlaceHolder1_txtUserName", "username"};
                string[] pfield = {"#pass", "#ctl00_ContentPlaceHolder1_txtPassword", "password" };
                string[] sbtn = { "#u_0_n", "#ctl00_ContentPlaceHolder1_btnLogin", ".btn-login" };
                string[] loginpage = { "", "", ""};

                for (var i = 0; i < names.Length; i++) {
                    cmd.CommandText = @"INSERT INTO application(name, type, username_field, password_field, login_page, submit_btn) VALUES (@nm,@tp,@usf,@pwf,@lp,@sbtn);";
                    cmd.Parameters.Add(new SQLiteParameter("@nm", names[i]));
                    cmd.Parameters.Add(new SQLiteParameter("@tp", types[i]));
                    cmd.Parameters.Add(new SQLiteParameter("@usf", ufield[i]));
                    cmd.Parameters.Add(new SQLiteParameter("@pwf", pfield[i]));
                    cmd.Parameters.Add(new SQLiteParameter("@sbtn", sbtn[i]));
                    cmd.Parameters.Add(new SQLiteParameter("@lp", loginpage[i]));

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }
        public String getUsernameFieldByWebsite(String website) {
            String sql = "SELECT username_field FROM application WHERE name=@name";
            SQLiteCommand cmd = new SQLiteCommand(sql, dbConn);
            cmd.Parameters.Add(new SQLiteParameter("@name", website));
            cmd.Prepare();

            SQLiteDataReader reader = cmd.ExecuteReader();
            String appUsernameField = "";
            if(reader.Read()) { 
                appUsernameField = (String)(reader["username_field"] != System.DBNull.Value ? reader["username_field"] : "");
            }
            return appUsernameField;
        }
        public String getPasswordFieldByWebsite(String website)
        {
            String sql = "SELECT password_field FROM application WHERE name=@name";
            SQLiteCommand cmd = new SQLiteCommand(sql, dbConn);
            cmd.Parameters.Add(new SQLiteParameter("@name", website));
            cmd.Prepare();

            SQLiteDataReader reader = cmd.ExecuteReader();
            String appPasswordField = "";
            if(reader.Read()) { 
              appPasswordField = (String)(reader["password_field"] != System.DBNull.Value ? reader["password_field"] : "");
            }
            return appPasswordField;
        }
        public String getAppID(String app)
        {
            String sql = "SELECT application_id FROM application WHERE name='" + app + "'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            String appID = (String)(reader["application_id"] != System.DBNull.Value ? reader["application_id"] : "");
            return appID;
        }

        public String getUsernameFieldById(String aid)
        {
            String sql = "SELECT username_field FROM application WHERE application_id='" + aid + "'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            String usrField = (String)(reader["username_field"] != System.DBNull.Value ? reader["username_field"] : "");
            return usrField;
        }

        public String getPasswordFieldById(String aid)
        {
            String sql = "SELECT password_field FROM application WHERE application_id='" + aid + "'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            String pwField = (String)(reader["password_field"] != System.DBNull.Value ? reader["password_field"] : "");
            return pwField;
        }
        public DataTable getAllApplications() {
            SQLiteCommand cmd = new SQLiteCommand(null, dbConn);
            cmd.CommandText = "SELECT application_id, name FROM application;";

            cmd.Prepare();
            SQLiteDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            return dt;
        }
    }
}