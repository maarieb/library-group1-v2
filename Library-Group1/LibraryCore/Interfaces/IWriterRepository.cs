using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.Interfaces
{
    public interface IWriterRepository
    {
        Task<List<Writer>> GetAll();
        Task<Writer> GetById(int id);


        Task<Writer> GetSingle(string lastname, string firstname);
        Task<List<Writer>> GetByName(string lastname);

        Task<bool> Exist(string lastname, string firstname);
        Task<bool> Exist(int id);
        Task Insert(Writer writer);
        Task Update(Writer writer);
        Task Delete(Writer writer);
    }
}
