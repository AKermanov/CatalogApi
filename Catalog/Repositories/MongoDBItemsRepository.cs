namespace Catalog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catalog.Entities;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongoDBItemsRepository : IInMemItemRepository
    {

        private const string DatabaseName = "catalog";
        private const string CollectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MongoDBItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(DatabaseName);
            this.itemsCollection = database.GetCollection<Item>(CollectionName);
        }

        public async Task CreateItemAsync(Item item)
        {
           await this.itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = this.filterBuilder.Eq(x => x.Id, id);
            await this.itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemsAsync(Guid id)
        {
            var filter = this.filterBuilder.Eq(item => item.Id, id);
            return await this.itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemAsync()
        {
            return await this.itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = this.filterBuilder.Eq(x => x.Id, item.Id);
            await this.itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}
