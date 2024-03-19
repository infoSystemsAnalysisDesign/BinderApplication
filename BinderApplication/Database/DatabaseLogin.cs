using MongoDB.Bson;
using MongoDB.Driver;

//SINGLETON CLASS
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

        internal void SaveLogin(string name, string email, string phoneNumber, string password)
        {
            var collection = database.GetCollection<BsonDocument>("Login");

            var document = new BsonDocument
            {
                { "Name", name},
                { "Email", email},
                { "Phone Number", phoneNumber},
                {"Password", password }
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
