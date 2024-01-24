using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.Interfaces
{
    public interface IReaderRepository
    {
        Task<List<Reader>> GetAll();
        Task<Reader> GetById(int id);
        Task<Reader> GetSingle(string lastname, string firstname);
        Task<List<Reader>> GetByName(string lastname);
        Task<bool> Exist(string lastname, string firstname);
        Task<bool> Exist(int id);
        Task Insert(Reader reader);
        Task Update(Reader reader);
        Task Delete(Reader reader);
    }
}
