using Library.Data;
using Library.Entities;
using LibraryCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfrastructure.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        public LibraryContext Context { get; set; }
        public ReaderRepository(LibraryContext context)
        {
            Context = context;
        }
        public async Task Delete(Reader reader)
        {
            Context.Readers.Remove(reader);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> Exist(string lastname, string firstname)
        {
            return await Context.Readers.AnyAsync(r => r.LastName.Equals(lastname) && r.FirstName.Equals(firstname));
        }

        public async Task<bool> Exist(int id)
        {
            return await Context.Readers.AnyAsync(r => r.Id.Equals(id));
        }

        public async Task<List<Reader>> GetAll()
        {
            return await Context.Readers.ToListAsync();
        }

        public async Task<Reader> GetById(int id)
        {
            return await Context.Readers.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Reader>> GetByName(string lastname)
        {
            return await Context.Readers.Where(r => r.LastName.Contains(lastname)).ToListAsync();
        }

        public async Task<Reader> GetSingle(string lastname, string firstname)
        {
            return await Context.Readers.FirstOrDefaultAsync(r => r.LastName.Equals(lastname) && r.FirstName.Equals(firstname));
        }

        public async Task Insert(Reader reader)
        {
            Context.Readers.Add(reader);
            await Context.SaveChangesAsync();
        }

        public async Task Update(Reader reader)
        {
            Context.Readers.Update(reader);
            await Context.SaveChangesAsync();
        }
    }
}
