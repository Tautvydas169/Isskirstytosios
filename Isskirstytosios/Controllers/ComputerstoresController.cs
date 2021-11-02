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
    [Route(template: "api/items/{itemsId}/computerstores")]
    public class ComputerstoresController : ControllerBase
    {


        private readonly IComputerstoresRepository _computerstoresRepository;
        private readonly IItemsRepository _itemsRepository;
        public ComputerstoresController(IComputerstoresRepository computerstoresRepository, IItemsRepository itemsRepository)
        {

            _computerstoresRepository = computerstoresRepository;
            _itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Computerstore>> GetAllAsync(int itemsId)
        {
            var computerstores = await _computerstoresRepository.GetAsync(itemsId);
            return computerstores;
        }

        [HttpGet(template: "{computerstoreId}")]
        public async Task<ActionResult<Computerstore>> GetAsync(int itemId, int computerstoreId)
        {
            var computerstore = await _computerstoresRepository.GetAsync(itemId, computerstoreId);
            if (computerstore == null) return NotFound();

            return Ok(computerstore);
        }

        [HttpPost]
        public async Task<ActionResult<Computerstore>> PostAsync(int itemId, Computerstore computerstore)
        {
            var item = await _itemsRepository.Get(itemId);
            if (item == null) return NotFound($"Item  with id '{itemId}' not found.");
            computerstore.ItemId = itemId;
            await _computerstoresRepository.InsertAsync(computerstore);
            return Created($"/api/items/{itemId}/computerstores/{computerstore.No}", computerstore);
        }


        [HttpPut(template: "{computerstoreId}")]
        public async Task<ActionResult<Client>> PutAsync(int itemId, int computerstoreId, Computerstore computerstore)
        {
            var item = await _itemsRepository.Get(itemId);
            if (item == null) return NotFound($"Item  with id '{itemId}' not found.");

            var oldcomputerstore = await _computerstoresRepository.GetAsync(itemId, computerstoreId);
            if (oldcomputerstore == null) return NotFound();


            await _computerstoresRepository.UpdateAsync(oldcomputerstore);
            return Ok(oldcomputerstore);
        }

        [HttpDelete(template: "{computerstoreId}")]
        public async Task<ActionResult> DeleteAsync(int itemId, int computerstoreId)
        {
            var computerstore = await _computerstoresRepository.GetAsync(itemId, computerstoreId);
            if (computerstore == null) return NotFound();

            await _computerstoresRepository.DeleteAsync(computerstore);
            return NoContent();
        }
    }
}
