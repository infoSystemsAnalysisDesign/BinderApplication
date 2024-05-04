using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BinderApplication.Database
{
    public class BsonStringCleaner
    {
        public static string CleanBsonString(string bsonString)
        {
            bsonString = Regex.Replace(bsonString, @"ObjectId\(""\w+""\)", "null");
            return bsonString;
        }
    }
}