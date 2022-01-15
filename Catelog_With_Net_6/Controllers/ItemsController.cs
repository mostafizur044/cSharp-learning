using Catelog.Dtos;
using Catelog.Entities;
using Catelog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catelog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _repositroy;

        public ItemsController(IItemsRepository repositroy)
        {
            _repositroy = repositroy;
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = _repositroy.GetItems().Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = _repositroy.GetItem(id);
            if(item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreateDate = DateTimeOffset.UtcNow
            };

            _repositroy.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var exsitingItem = _repositroy.GetItem(id);

            if(exsitingItem is null){
                return NotFound();
            }

            Item updatedItem = exsitingItem with {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            _repositroy.UpdateItem(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var exsitingItem = _repositroy.GetItem(id);

            if(exsitingItem is null){
                return NotFound();
            }

            _repositroy.DeleteItem(id);

            return NoContent();
        }
    }
    
}