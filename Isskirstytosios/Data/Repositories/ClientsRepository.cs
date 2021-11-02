using Isskirstytosios.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isskirstytosios.Data.Repositories
{

    public interface IClientsRepository
    {
        Task<Client> GetAsync(int computerstoreId, int clientId);
        Task<List<Client>> GetAsync(int clientId);
        Task InsertAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Client client);
    }

    public class ClientsRepository : IClientsRepository
    {
        private readonly Context _context;
        public ClientsRepository(Context context)
        {
            _context = context;
        }

        public async Task<Client> GetAsync(int computerstoreId, int clientId)
        {
            return await _context.Clients.FirstOrDefaultAsync(o => o.ComputerstoreId == computerstoreId && o.No == clientId);
        }

        public async Task<List<Client>> GetAsync(int clientId)
        {
            return await _context.Clients.Where(o => o.No == clientId).ToListAsync();
        }

        public async Task InsertAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Client client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}
