using System;
using System.Collections.Generic;
using DotNetCoreApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DotNetCoreApi.Context
{
    public class DbContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public DbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            _mongoDatabase = new MongoClient(connectionString).GetDatabase("DotNetCore");
        }

        public void Insert<T>(T entity) where T : BaseModel
        {
            entity.Id = GetNextId<T>();

            var collection = GetCollection<T>();

            collection.InsertOne(entity);
        }

        public void Update<T>(T entity) where T : BaseModel
        {
            var collection = GetCollection<T>();
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);

            collection.ReplaceOne(filter, entity);
        }

        public void Delete<T>(int id) where T : BaseModel
        {
            var collection = GetCollection<T>();
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);

            collection.DeleteOne(filter);
        }

        public T Get<T>(int id) where T : BaseModel
        {
            var collection = GetCollection<T>();
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);

            return collection.Find<T>(filter).FirstOrDefault();
        }

        public List<T> List<T>() where T : BaseModel
        {
            var collection = GetCollection<T>();
            var filter = Builders<T>.Filter.Empty;

            return collection.Find<T>(filter).ToList();
            //return collection.AsQueryable<T>().ToList<T>();
        }

        public int GetNextId<T>() where T : BaseModel
        {
            var returnId = 0;
            var collectionName = GetCollectionName(typeof(T));
            var filter = Builders<Counter>.Filter.Eq(x => x.CollectionName, collectionName);

            var counterCollection = GetCollection<Counter>();
            var counter = counterCollection.Find<Counter>(filter).FirstOrDefault();
            if (counter == null)
            {
                counter = new Counter() { CollectionName = collectionName, NextId = 1 };
                returnId = counter.NextId;

                counter.NextId++;
                counterCollection.InsertOne(counter);
            }
            else
            {
                returnId = counter.NextId;
                
                counter.NextId++;
                counterCollection.ReplaceOne(filter, counter);
            }

            return returnId;
        }

        private string GetCollectionName(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(MongoCollectionAttribute), false);
            return attributes.Length > 0 ? ((MongoCollectionAttribute)attributes[0]).CollectionName : "temp";
        }

        private IMongoCollection<T> GetCollection<T>()
        {
            return _mongoDatabase.GetCollection<T>(GetCollectionName(typeof(T)));
        }
    }
}
