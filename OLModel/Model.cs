using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace OLModel
{
    public class Model
    {
        // TODO: Save stuff on setters
        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<String> AdminLog { get; set; }
        public List<String> UserLog { get; set; }

        public static int MODEL_VERSION = 1;
        private static int LOG_ADMIN_ONLY = 1; // everything <= is admin only (see note below)

        private void createNewDatabase(string filename)
        {
            Console.WriteLine(String.Format("Creating database \"{0}\"", filename));
            SQLiteConnection.CreateFile(filename);
            string connStr = String.Format("Data Source={0};Version=3;", filename);
            SQLiteConnection dbConn = new SQLiteConnection(connStr);
            dbConn.Open();
            string[] statements = {
                "CREATE TABLE IF NOT EXISTS users(studentID TEXT NOT NULL PRIMARY KEY, studentName TEXT);",
                "CREATE TABLE IF NOT EXISTS products(productID TEXT NOT NULL PRIMARY KEY, productName TEXT, productImage TEXT, price INT);",
                "CREATE TABLE IF NOT EXISTS transactions(studentID TEXT, productID TEXT, amount INT);", // TODO: add time
                "CREATE TABLE IF NOT EXISTS settings(key TEXT NOT NULL PRIMARY KEY, val INT);",
                "CREATE TABLE IF NOT EXISTS logs(time TEXT NOT NULL, permission INT, event TEXT);",

                "INSERT INTO users VALUES (1337, 'Admin');",
                "INSERT INTO products VALUES (5708429004221, 'Svaneke Grunge IPA', 'svaneke.jpg', 20);",
                "INSERT INTO settings VALUES ('version', "+MODEL_VERSION+");",
                "INSERT INTO logs VALUES ('" + Helpers.getTimeStamp() + "', 1, 'Created new database');"
            };
            foreach (string stmt in statements)
            {
                SQLiteCommand command = new SQLiteCommand(stmt, dbConn);
                Console.WriteLine(String.Format("SQL: {0}", stmt));
                command.ExecuteNonQuery();
            }
            //dbConn.Commit();
            dbConn.Close();
        }

        private int getVersion (SQLiteConnection dbConn)
        {
            SQLiteCommand command = new SQLiteCommand("SELECT val FROM settings WHERE key = 'version' LIMIT 1", dbConn);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.HasRows && reader.Read())
            {
                int version = reader.GetInt32(0);
                /*if (!Int32.TryParse(reader["version"].ToString(), out version))
                    return -1;*/

                return version;
            }
            return -1;
        }

        private Model() {
            string filename = "testdb.sqlite3";
            if (!System.IO.File.Exists(filename))
                createNewDatabase(filename);

            string connStr = String.Format("Data Source={0};Version=3;", filename);
            SQLiteConnection dbConn = new SQLiteConnection(connStr);
            dbConn.Open();

            int version = getVersion(dbConn);
            if (version != MODEL_VERSION)
            {
                Console.WriteLine(String.Format("Unknown database version {0} for file \"{1}\"", version, filename));
                return; // TODO: Kill?
            }

            Users = new List<User>();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM users", dbConn);
            SQLiteDataReader reader = command.ExecuteReader();
            #region users
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string studentID = reader["studentID"].ToString();
                    string studentName = reader["studentName"].ToString();
                    Users.Add( new User(studentID, studentName) );
                }
            }
            #endregion

            Products = new List<Product>();
            command = new SQLiteCommand("SELECT * FROM products", dbConn);
            reader = command.ExecuteReader();
            #region products
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string productId = reader["productID"].ToString();
                    string productName = reader["productName"].ToString();
                    string productImage = reader["productImage"].ToString(); 

                    int price;
                    if (!Int32.TryParse(reader["price"].ToString(), out price))
                        price = -1;

                    Products.Add(
                        "".Equals(productImage)
                        ? new Product(productId, productName, price)
                        : new Product(productId, productName, price, productImage)
                    );

                }
            }
            #endregion

            Transactions = new List<Transaction>();
            command = new SQLiteCommand("SELECT * FROM transactions", dbConn);
            reader = command.ExecuteReader();
            #region transactions
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //transactions(studentID TEXT, productID TEXT, amount INT);", // TODO: add time
                    string studentId = reader["studentID"].ToString();
                    string productId = reader["productID"].ToString();
                    int amount;
                    if (!Int32.TryParse(reader["amount"].ToString(), out amount))
                        amount = 0;

                    Transactions.Add( new Transaction(studentId, productId, amount) );
                }
            }
            #endregion


            AdminLog = new List<String>();
            UserLog = new List<String>();
            command = new SQLiteCommand("SELECT time, permission, event FROM logs", dbConn);
            reader = command.ExecuteReader();
            #region log
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string time = reader["time"].ToString();
                    string eventName = reader["event"].ToString();

                    int permissions;
                    if (!Int32.TryParse(reader["permission"].ToString(), out permissions))
                        permissions = Int32.MaxValue;

                    if (LOG_ADMIN_ONLY <= permissions) // TODO: refactor logic
                    {
                        AdminLog.Add(string.Format("{0}: {1}", time, eventName));
                    } else
                    {
                        UserLog.Add(string.Format("{0}: {1}", time, eventName));
                    }
                }
            }
            #endregion

        }

        // Singleton pattern <3
        private static Model instance;
        public static Model Instance { get { if (instance == null) instance = new Model(); return instance; } }
    }
}
