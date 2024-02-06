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
    public class BookRepository : IBookRepository
    {
        public LibraryContext Context { get; }
        public BookRepository(LibraryContext context)
        {
            Context = context;
        }
        public async Task Delete(Book book)
        {
            Context.Books.Remove(book);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> Exist(string title)
        {
            return await Context.Books.AnyAsync(x => x.Title.Equals(title));
        }

        public async Task<bool> Exist(int id)
        {
            return await Context.Books.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Book>> GetAll()
        {
            return await Context.Books.ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
            return await Context.Books.Include(b => b.Domain).Include(b => b.Writer).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> GetByTitle(string title)
        {
            return await Context.Books.FirstOrDefaultAsync(b => b.Title.Equals(title));
        }

        public async Task Insert(Book book)
        {
            Context.Books.Add(book);
            await Context.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
            Context.Books.Update(book);
            await Context.SaveChangesAsync();
        }
    }
}
