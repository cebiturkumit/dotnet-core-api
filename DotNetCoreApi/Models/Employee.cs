namespace DotNetCoreApi.Models
{
    [MongoCollection("Employees")]
    public class Employee : BaseModel
    {
        public string Name { get; set; }
    }
}
