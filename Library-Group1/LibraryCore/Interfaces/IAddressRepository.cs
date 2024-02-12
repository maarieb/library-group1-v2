using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAll();
        Task<Address> GetById(int id);


        Task<Address> GetSingle(Address address);
        Task<List<Address>> GetByCity(string city);
        Task<List<Address>> GetByCountry(string country);
        Task<List<Address>> GetByStreet(string street);
        Task<bool> Exist(int id);

        Task Insert(Address address);
        Task Update(Address address);
        Task Delete(Address address);
    }
}
