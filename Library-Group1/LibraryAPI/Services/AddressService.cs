using Library.Entities;
using LibraryCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class AddressService
    {
        private readonly IAddressRepository Repository;

        public AddressService(IAddressRepository repo)
        {
            Repository = repo;
        }

        public async Task<List<Address>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task <Address> GetById(int? id)
        {
            var address = await Repository.GetById(id.Value);
            return address;
        }


        // A voir si necessaire, CRUD complet pour adresse ? 
        public async Task<Address> Create(Address address)
        {
            await Repository.Insert(address);
            return address;
        }

        public async Task<Address> Update(Address address)
        {
            await Repository.Update(address);
            return address;
        }

        public async Task Delete(Address address)
        {
            await Repository.Delete(address);
        }

        public async Task<Address> GetSingle(Address address)
        {
            return await Repository.GetSingle(address);
        }
    }
}
