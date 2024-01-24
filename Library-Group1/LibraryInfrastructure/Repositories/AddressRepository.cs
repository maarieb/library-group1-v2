using Library.Data;
using Library.Entities;
using LibraryCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {

        public LibraryContext Context { get; }
        public AddressRepository(LibraryContext context)
        {
            Context = context;
        }

        public async Task Delete(Address address)
        {
            Context.Addresses.Remove(address);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> Exist(string appartment, string street, string city, string zipCode, string country)
        {
            return await Context.Addresses.AnyAsync(a => a.Appartment.Equals(appartment) &&
            a.Street.Equals(street) && a.City.Equals(city) && a.ZipCode.Equals(zipCode) && a.Country.Equals(country));
        }

        public async Task<bool> Exist(int id)
        {
            return await Context.Addresses.AnyAsync(a => a.Id == id);
        }

        public async Task<List<Address>> GetAll()
        {
            return await Context.Addresses.ToListAsync();
        }

        public async Task<List<Address>> GetByCity(string city)
        {
            return await Context.Addresses.Where(a => a.City.Contains(city)).ToListAsync();
        }

        public async Task<List<Address>> GetByCountry(string country)
        {
            return await Context.Addresses.Where(a => a.Country.Contains(country)).ToListAsync();
        }

        public async Task<Address> GetById(int id)
        {
            return await Context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Address>> GetByStreet(string street)
        {
            return await Context.Addresses.Where(a => a.Street.Contains(street)).ToListAsync();
        }

        public async Task<Address> GetSingle(string appartment, string street, string city, string zipCode, string country)
        {
            return await Context.Addresses.FirstOrDefaultAsync(a => a.Appartment.Equals(appartment) &&
            a.Street.Equals(street) && a.City.Equals(city) && a.ZipCode.Equals(zipCode) && a.Country.Equals(country));
        }

        public async Task Insert(Address address)
        {
            Context.Addresses.Add(address);
            await Context.SaveChangesAsync();
        }

        public async Task Update(Address address)
        {
            Context.Addresses.Update(address);
            await Context.SaveChangesAsync();
        }
    }
}
