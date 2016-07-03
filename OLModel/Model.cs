using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;

namespace OLModel
{
    public class Model
    {
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Transaction> Transactions { get; } = new ObservableCollection<Transaction>();
        public List<String> AdminLog { get; set; }
        public List<String> UserLog { get; set; }

        public const int MODEL_VERSION = 1;
        private const int LOG_ADMIN_ONLY = 1; // everything <= is admin only (see note below)

        private SQLiteConnection dbConn;
        private const string dbFilename = "testdb.sqlite";
        private const string connStr = "Data Source={0};Version=3;";

        private bool createNewDatabase()
        {
            Console.WriteLine(String.Format("Creating database \"{0}\"", dbFilename));
            SQLiteConnection.CreateFile(dbFilename);
            dbConn = new SQLiteConnection(String.Format(connStr, dbFilename));
            dbConn.Open();
            string[] statements = {
                "CREATE TABLE IF NOT EXISTS users(studentID TEXT NOT NULL PRIMARY KEY, studentName TEXT);",
                "CREATE TABLE IF NOT EXISTS products(productID TEXT NOT NULL PRIMARY KEY, productName TEXT, productImage TEXT, price INT, stock INT);",
                "CREATE TABLE IF NOT EXISTS transactions(studentID TEXT, productID TEXT, amount INT);", // TODO: add time
                "CREATE TABLE IF NOT EXISTS settings(key TEXT NOT NULL PRIMARY KEY, val INT);",
                "CREATE TABLE IF NOT EXISTS logs(time TEXT NOT NULL, permission INT, event TEXT);",

                "INSERT INTO users VALUES (1337, 'Admin');",
                "INSERT INTO products VALUES (5708429004221, 'Svaneke Grunge IPA', 'svaneke.jpg', 20, 0);",
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
            return true;
        }

        private int getVersion()
        {
            if (!OpenConnection()) return -1;

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

        private bool OpenConnection()
        {
            if (dbConn != null && dbConn.State == ConnectionState.Open)
                return true; // Connection already open

            if (!System.IO.File.Exists(dbFilename))
                return createNewDatabase();

            dbConn = new SQLiteConnection(String.Format(connStr, dbFilename));
            dbConn.Open();

            return true;
        }

        private Model() {
            Users = new ObservableCollection<User>();
            Products = new ObservableCollection<Product>();
            AdminLog = new List<String>();
            UserLog = new List<String>();

            if (!OpenConnection()) return; // TODO: Kill?

            int version = getVersion();
            if (version != MODEL_VERSION)
            {
                Console.WriteLine(String.Format("Unknown database version {0} for file \"{1}\"", version, dbFilename));
                return; // TODO: Kill?
            }

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
            Users.CollectionChanged += UsersAddedOrRemoved; // Warning: Add _after_ filling list
            #endregion

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
                        price = 10000; // This should be noticeable

                    int stock;
                    if (!Int32.TryParse(reader["stock"].ToString(), out stock))
                        stock = 0;

                    Products.Add( new Product(productId, productName, price, stock, productImage) );

                }
            }
            Products.CollectionChanged += ProductsAddedOrRemoved; // Warning: Add _after_ filling list
            #endregion

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
            Transactions.CollectionChanged += TransactionAdded;
            #endregion

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

        private void UsersAddedOrRemoved(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (User user in e.NewItems)
                {
                    string stmt = "INSERT OR REPLACE INTO users (studentID, studentName) VALUES (@id, @name)";
                    SQLiteCommand command = new SQLiteCommand(stmt, dbConn);
                    command.Parameters.AddWithValue("id", user.UserID);
                    command.Parameters.AddWithValue("name", user.Name);
                    Console.WriteLine(String.Format("Added/updated user {0}.", user));
                    command.ExecuteNonQuery();
                    //user.PropertyChanged += UsersAddedOrRemoved;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (User user in e.OldItems)
                {
                    string stmt = "DELETE FROM users WHERE studentID = @id AND studentName = @name"; // TODO: make sure to "LIMIT 1"
                    SQLiteCommand command = new SQLiteCommand(stmt, dbConn);
                    command.Parameters.AddWithValue("id", user.UserID);
                    command.Parameters.AddWithValue("name", user.Name);
                    Console.WriteLine(String.Format("SQL: {0}", stmt));
                    command.ExecuteNonQuery();
                    //user.PropertyChanged -= UsersAddedOrRemoved;
                }
            }
        }
        private void ProductsAddedOrRemoved(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Product product in e.NewItems)
                {
                    string stmt = "INSERT OR REPLACE INTO products (productID, productName, productImage, price) VALUES (@id, @name, @img, @price)";
                    SQLiteCommand command = new SQLiteCommand(stmt, dbConn);
                    command.Parameters.AddWithValue("id", product.ProductId);
                    command.Parameters.AddWithValue("name", product.ProductName);
                    command.Parameters.AddWithValue("img", product.ImageFileName);
                    command.Parameters.AddWithValue("price", product.Price);
                    Console.WriteLine(String.Format("Added/updated product {0}.", product));
                    command.ExecuteNonQuery();
                    //product.PropertyChanged += ProductsAddedOrRemoved;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Product product in e.OldItems)
                {
                    string stmt = "DELETE FROM products WHERE productID = @id AND productName = @name AND productImage = @img AND price = @price"; // TODO: make sure to "LIMIT 1"
                    SQLiteCommand command = new SQLiteCommand(stmt, dbConn);
                    command.Parameters.AddWithValue("id", product.ProductId);
                    command.Parameters.AddWithValue("name", product.ProductName);
                    command.Parameters.AddWithValue("img", product.ImageFileName);
                    command.Parameters.AddWithValue("price", product.Price);
                    Console.WriteLine(String.Format("SQL: {0}", stmt));
                    command.ExecuteNonQuery();
                    //product.PropertyChanged += ProductsAddedOrRemoved;
                }
            }
        }
        private void TransactionAdded(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Transaction t in e.NewItems)
                {
                    string stmt = "INSERT INTO transactions (studentID, productID, amount) VALUES (@sID, @pID, @amount)";
                    SQLiteCommand command = new SQLiteCommand(stmt, dbConn);
                    command.Parameters.AddWithValue("sID", t.studentId);
                    command.Parameters.AddWithValue("pID", t.productId);
                    command.Parameters.AddWithValue("amount", t.amount);
                    Console.WriteLine(String.Format("Transaction: {0}.", t));
                    command.ExecuteNonQuery();
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Transaction t in e.OldItems)
                {
                    string log = String.Format("{0} - WARNING: Transaction '{1}' deleted!", Helpers.getTimeStamp(), t);
                    AdminLog.Add(log);
                    Console.WriteLine(log);
                }
            }
        }


        ~Model()
        {
            Console.WriteLine(String.Format("{0} - Model shutdown.", Helpers.getTimeStamp()));
        }

        // Singleton pattern <3
        private static Model instance;
        public static Model Instance { get { if (instance == null) instance = new Model(); return instance; } }
    }
}
