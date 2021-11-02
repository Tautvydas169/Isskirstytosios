using Isskirstytosios.Data.Repositories;
using Isskirstytosios.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isskirstytosios.Controllers
{
    [ApiController]
    [Route(template: "api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _itemsRepository;
        public ItemsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _itemsRepository.GetAll();
        }

        [HttpGet(template: "{id}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            var _item = await _itemsRepository.Get(id);
            if (_item == null) return NotFound($"Item with id '{id}' not found.");
            return Ok(await _itemsRepository.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult<Item>> Post(Item item)
        {
            await _itemsRepository.Create(item);
            //201
            //Created
            return Created($"/api/items/{item.No}", item);
        }

        [HttpPut(template: "{id}")]
        public async Task<ActionResult<Item>> Put(int id, Item item)
        {
            var _item = await _itemsRepository.Get(id);
            if (_item == null) return NotFound($"Item with id '{id}' not found.");
            _item.Name = item.Name;
            await _itemsRepository.Put(_item);
            return Ok(await _itemsRepository.Get(id));
        }

        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<Item>> Delete(int id)
        {
            var _item = await _itemsRepository.Get(id);
            if (_item == null) return NotFound($"Item with id '{id}' not found.");
            await _itemsRepository.Delete(_item);
            //204
            return NoContent();
        }
    }
}
