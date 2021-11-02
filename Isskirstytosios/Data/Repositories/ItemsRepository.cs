using Isskirstytosios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isskirstytosios.Data.Repositories
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> GetAll();
        Task<Item> Get(int id);
        Task<Item> Create(Item item);
        Task<Item> Put(Item item);
        Task Delete(Item item);
    }
    public class ItemsRepository : IItemsRepository
    {
        private readonly Context _context;

        public ItemsRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            var item = _context.Items.ToList();
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> Get(int id)
        {
            var item = _context.Items.Where(x => x.No == id).FirstOrDefault();
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Item> Create(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Item> Put(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task Delete(Item item)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

        }
    }
}
