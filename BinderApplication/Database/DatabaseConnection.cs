using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

using Microsoft.Maui.Controls;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

//SINGLETON CLASS
namespace BinderApplication.Database
{
    public class DatabaseConnection
    {
        private static readonly Lazy<DatabaseConnection> instance = new Lazy<DatabaseConnection>(() => new DatabaseConnection());
        private readonly IMongoDatabase database;

        public DatabaseConnection()
        {
            const string connectionUri = "mongodb://Binder:AlsoBinder1@ac-clelo6g-shard-00-00.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-01.ibrxa6e.mongodb.net:27017,ac-clelo6g-shard-00-02.ibrxa6e.mongodb.net:27017/?ssl=true&replicaSet=atlas-i5m36b-shard-0&authSource=admin&retryWrites=true&w=majority";

            var settings = MongoClientSettings.FromConnectionString(connectionUri);

            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Create a new client and connect to the server
            var client = new MongoClient(settings);

            // Send a ping to confirm a successful connection
            try
            {
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Debug.WriteLine("Pinged your deployment. You successfully connected to Binder!");
                database = client.GetDatabase("Binder");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Connection Failed. Exception thrown on Console.");
                Debug.WriteLine(ex);
            }
        }

        public static DatabaseConnection Instance => instance.Value;

        public IMongoDatabase GetDatabase()
        {
            return database;
        }
    }

}