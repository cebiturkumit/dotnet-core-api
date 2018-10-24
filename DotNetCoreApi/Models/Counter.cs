using MongoDB.Bson;

namespace DotNetCoreApi.Models
{
    [MongoCollection("Counters")]
    public class Counter
    {
        public ObjectId Id { get; set; }
        public string CollectionName { get; set; }
        public int NextId { get; set; }
    }
}