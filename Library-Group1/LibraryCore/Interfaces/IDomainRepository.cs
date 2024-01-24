using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.Interfaces
{
    public interface IDomainRepository
    {
        Task<List<Domain>> GetAll();
        Task<Domain> GetById(int id);
        Task<Domain> GetByName(string name);
        Task<bool> Exist(string name);
        Task<bool> Exist(int id);
        Task Insert(Domain domain);
        Task Update(Domain domain);
        Task Delete(Domain domain);
    }
}
