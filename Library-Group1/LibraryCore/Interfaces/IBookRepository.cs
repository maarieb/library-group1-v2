using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> GetByTitle(string title);
        Task<bool> Exist(string title);
        Task<bool> Exist(int id);
        Task Insert(Book book);
        Task Update(Book book);
        Task Delete(Book book);
    }
}
