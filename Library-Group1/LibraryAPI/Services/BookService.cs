using Library.Entities;
using LibraryCore.Interfaces;

namespace LibraryAPI.Services
{
    public class BookService
    {
        private readonly IBookRepository Repository;

        public BookService(IBookRepository repo)
        {
            Repository = repo;
        }

        public async Task<List<Book>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task<Book> GetById(int? id)
        {
            var book = await Repository.GetById(id.Value);
            return book;
        }
        public async Task<Book> Create(Book book)
        {
            await Repository.Insert(book);
            return book;
        }

        public async Task<Book> Update(Book book)
        {
            await Repository.Update(book);
            return book;
        }

        public async Task Delete(Book book)
        {
            await Repository.Delete(book);
        }

        public async Task<Book> GetSingle(string title)
        {
            return await Repository.GetByTitle(title);
        }
    }
}
