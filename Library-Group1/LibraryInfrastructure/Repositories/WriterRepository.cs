using Library.Data;
using Library.Entities;
using LibraryCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfrastructure.Repositories
{
    public class WriterRepository : IWriterRepository
    {
        public LibraryContext Context { get; }

        public WriterRepository(LibraryContext context)
        {
            Context = context;
        }


        public async Task Delete(Writer writer)
        {
            Context.Writers.Remove(writer);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Writer>> GetAll()
        {
            return await Context.Writers.ToListAsync();
        }

        public async Task<Writer> GetById(int id)
        {
            return await Context.Writers.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<Writer>> GetByName(string lastname)
        {
            return await Context.Writers.Where(w => w.LastName.Contains(lastname)).ToListAsync();
        }

        public async Task<bool> Exist(string lastname, string firstname)
        {
            return await Context.Writers.AnyAsync(w => w.LastName.Equals(lastname) && w.FirstName.Equals(firstname));
        }
        public async Task<bool> Exist(int id)
        {
            return await Context.Writers.AnyAsync(w => w.Id.Equals(id));
        }

        public async Task<Writer> GetSingle(string lastname, string firstname)
        {
            return await Context.Writers.FirstOrDefaultAsync(w => w.LastName.Equals(lastname) && w.FirstName.Equals(firstname));
        }

        public async Task Insert(Writer writer)
        {
            Context.Add(writer);
            await Context.SaveChangesAsync();
        }

        public async Task Update(Writer writer)
        {
            Context.Update(writer); 
            await Context.SaveChangesAsync();
        }
    }
}
