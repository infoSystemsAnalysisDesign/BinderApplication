//using Amazon.Runtime.Internal.Util;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using System.Security.Cryptography;

////SINGLETON CLASS
//namespace BinderApplication.Database
//{
//    public class DatabaseLogin
//    {
//        private static readonly Lazy<DatabaseLogin> instance = new Lazy<DatabaseLogin>(() => new DatabaseLogin());
//        private readonly IMongoDatabase database;

//        private DatabaseLogin()
//        {
//            this.database = DatabaseConnection.Instance.GetDatabase();
//        }

//        public static DatabaseLogin Instance => instance.Value;

//        internal void SaveLogin(string name, string email, string phoneNumber, string password)
//        {
//            var collection = database.GetCollection<BsonDocument>("Login");

//            var document = new BsonDocument
//            {
//                { "Name", name},
//                { "Email", email},
//                { "Phone Number", phoneNumber},
//                {"Password", password }
//            };

//            collection.InsertOne(document);
//        }

//        private string email = "", password = "";
//        public void StoreLogin(string emailFromLogin, string passwordFromLogin)
//        {
//            email = emailFromLogin;
//            password = passwordFromLogin;
//        }
//        public string GetEmail() { return email; }
//        public string GetPassword() { return password; }
//    }
//}
using Amazon.Runtime.Internal.Util;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace BinderApplication.Database
{
    public class DatabaseLogin
    {
        private static readonly Lazy<DatabaseLogin> instance = new Lazy<DatabaseLogin>(() => new DatabaseLogin());
        private readonly IMongoDatabase database;

        private DatabaseLogin()
        {
            this.database = DatabaseConnection.Instance.GetDatabase();
        }

        public static DatabaseLogin Instance => instance.Value;

        //calculate SHA256 hash
        //https://www.thatsoftwaredude.com/content/6218/how-to-encrypt-passwords-using-sha-256-in-c-and-net where i found this
        public byte[] CalculateSHA256(string str)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue;
                hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
                return hashValue;
            }
        }

        // save login information with hashed password
        internal void SaveLogin(string name, string email, string phoneNumber, string password)
        {
            var collection = database.GetCollection<BsonDocument>("Login");

            // Hash the password before saving
            byte[] hashedPassword = CalculateSHA256(password);
            string hashedPasswordString = BitConverter.ToString(hashedPassword).Replace("-", "");

            var document = new BsonDocument
            {
                { "Name", name},
                { "Email", email},
                { "Phone Number", phoneNumber},
                {"Password", hashedPasswordString } // Store hashed password
            };

            collection.InsertOne(document);
        }

        private string email = "", password = "";
        public void StoreLogin(string emailFromLogin, string passwordFromLogin)
        {
            email = emailFromLogin;
            password = passwordFromLogin;
        }
        public string GetEmail() { return email; }
        public string GetPassword() { return password; }
    }
}

