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
    [Route(template: "api/computerstores/{computerstoreId}/clients")]
    public class ClientsController : ControllerBase
    {

        private readonly IClientsRepository _clientsRepository;
        private readonly IComputerstoresRepository _computerstoresRepository;
        public ClientsController(IClientsRepository clientsRepository, IComputerstoresRepository computerstoresRepository)
        {

            _clientsRepository = clientsRepository;
            _computerstoresRepository = computerstoresRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> GetAllAsync(int computerstoresId)
        {
            var clients = await _clientsRepository.GetAsync(computerstoresId);
            return clients;
        }

        [HttpGet(template: "{clientId}")]
        public async Task<ActionResult<Client>> GetAsync(int computerstoreId, int clientId)
        {
            var client = await _clientsRepository.GetAsync(computerstoreId, clientId);
            if (client == null) return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> PostAsync(int computerstoreId, Client client)
        {
            var computerstore = await _computerstoresRepository.GetAsync(computerstoreId);
            if (computerstore == null) return NotFound($"Computer store with id '{computerstoreId}' not found.");
            client.ComputerstoreId = computerstoreId;
            await _clientsRepository.InsertAsync(client);
            return Created($"/api/computerstores/{computerstoreId}/clients/{client.No}", client);
        }


        [HttpPut(template: "{clientId}")]
        public async Task<ActionResult<Client>> PutAsync(int computerstoreId, int clientId, Client client)
        {
            var computerstore = await _computerstoresRepository.GetAsync(computerstoreId);
            if (computerstore == null) return NotFound($"Computer store with id '{computerstoreId}' not found.");

            var oldClient = await _clientsRepository.GetAsync(computerstoreId, clientId);
            if (oldClient == null) return NotFound();


            await _clientsRepository.UpdateAsync(oldClient);
            return Ok(oldClient);
        }

        [HttpDelete(template: "{clientId}")]
        public async Task<ActionResult> DeleteAsync(int computerstoreId, int clientId)
        {
            var client = await _clientsRepository.GetAsync(computerstoreId, clientId);
            if (client == null) return NotFound();

            await _clientsRepository.DeleteAsync(client);
            return NoContent();
        }
    }
}
