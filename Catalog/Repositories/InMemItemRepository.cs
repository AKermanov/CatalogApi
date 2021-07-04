
namespace Catalog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Catalog.Entities;

    public class InMemItemRepository : IInMemItemRepository
    {
        private readonly List<Item> items = new ()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow },
        };

        public async Task<IEnumerable<Item>> GetItemAsync()
        {
            return await Task.FromResult(this.items);
        }

        public async Task<Item> GetItemsAsync(Guid id)
        {
            var item = this.items.Where(x => x.Id == id).FirstOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            this.items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = this.items.FindIndex(x => x.Id == item.Id);
            this.items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = this.items.FindIndex(x => x.Id == id);
            this.items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}
