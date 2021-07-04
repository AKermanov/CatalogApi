namespace Catalog.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catalog.Entities;

    public interface IInMemItemRepository
    {
       Task<Item> GetItemsAsync(Guid id);

       Task<IEnumerable<Item>> GetItemAsync();

       Task CreateItemAsync(Item item);

       Task UpdateItemAsync(Item item);

       Task DeleteItemAsync(Guid id);
    }
}
