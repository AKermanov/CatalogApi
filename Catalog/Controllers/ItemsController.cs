namespace Catalog.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Catalog.Dtos;
    using Catalog.Entities;
    using Catalog.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IInMemItemRepository repository;

        public ItemsController(IInMemItemRepository repository)
        {
            this.repository = repository;
        }

        // https://youtu.be/ZXdFisA_hOY 3:00:00
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            return (await this.repository.GetItemAsync())
                                         .Select(x => x.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemsAsync(Guid id)
        {
            var item = await this.repository.GetItemsAsync(id);

            if (item is null)
            {
                return this.NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            var item = new Item
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            await this.repository.CreateItemAsync(item);

            return this.CreatedAtAction(nameof(this.GetItemsAsync), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await this.repository.GetItemsAsync(id);

            if (existingItem is null)
            {
                return this.NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
            };

            await this.repository.UpdateItemAsync(updatedItem);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await this.repository.GetItemsAsync(id);

            if (existingItem is null)
            {
                return this.NotFound();
            }

            await this.repository.DeleteItemAsync(id);

            return this.NoContent();
        }
    }
}
