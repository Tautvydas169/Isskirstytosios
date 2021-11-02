using Isskirstytosios.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isskirstytosios.Data.Repositories
{
    public interface IComputerstoresRepository
    {
        Task<Computerstore> GetAsync(int itemId, int computerstoreId);
        Task<List<Computerstore>> GetAsync(int markeId);
        Task InsertAsync(Computerstore computerstore);
        Task UpdateAsync(Computerstore computerstore);
        Task DeleteAsync(Computerstore computerstore);
    }

    public class ComputerstoresRepository : IComputerstoresRepository
    {
        private readonly Context _context;
        public ComputerstoresRepository(Context context)
        {
            _context = context;
        }

        public async Task<Computerstore> GetAsync(int itemId, int computerstoreId)
        {
            return await _context.Computerstores.FirstOrDefaultAsync(o => o.ItemId == itemId && o.No == computerstoreId);
        }

        public async Task<List<Computerstore>> GetAsync(int itemId)
        {
            return await _context.Computerstores.Where( o => o.ItemId == itemId).ToListAsync();
        }

        public async Task InsertAsync(Computerstore computerstore)
        {
            _context.Computerstores.Add(computerstore);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Computerstore computerstore)
        {
            _context.Computerstores.Update(computerstore);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Computerstore computerstore)
        {
            _context.Computerstores.Remove(computerstore);
            await _context.SaveChangesAsync();
        }
    }
}
