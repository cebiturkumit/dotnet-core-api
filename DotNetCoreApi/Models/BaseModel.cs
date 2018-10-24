using System;

namespace DotNetCoreApi.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
    }

    public class MongoCollectionAttribute : Attribute
    {
        public MongoCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }

        public string CollectionName { get; set; }
    }
}
