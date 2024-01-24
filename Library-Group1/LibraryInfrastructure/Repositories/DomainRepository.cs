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
    public class DomainRepository : IDomainRepository
    {
        public LibraryContext Context { get; }
        public DomainRepository (LibraryContext context)
        {
            Context = context;
        }

        public async Task Delete(Domain domain)
        {
            Context.Domains.Remove(domain);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> Exist(string name)
        {
            return await Context.Domains.AnyAsync(d => d.Name.Equals(name));
        }

        public async Task<bool> Exist(int id)
        {
            return await Context.Domains.AnyAsync(d => d.Id == id);
        }

        public async Task<List<Domain>> GetAll()
        {
            return await Context.Domains.ToListAsync();
        }

        public async Task<Domain> GetById(int id)
        {
            return await Context.Domains.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Domain> GetByName(string name)
        {
            return await Context.Domains.FirstOrDefaultAsync(d => d.Name.Equals(name));
        }

        public async Task Insert(Domain domain)
        {
            Context.Domains.Add(domain);
            await Context.SaveChangesAsync();
        }

        public async Task Update(Domain domain)
        {
            Context.Domains.Update(domain);
            await Context.SaveChangesAsync();
        }
    }
}
