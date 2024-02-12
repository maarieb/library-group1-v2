using Library.Entities;
using LibraryCore.Interfaces;

namespace LibraryAPI.Services
{
    public class DomainService
    {
        private readonly IDomainRepository Repository;

        public DomainService(IDomainRepository repo)
        {
            Repository = repo;
        }

        public async Task<List<Domain>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task<Domain> GetById(int? id)
        {
            var domain = await Repository.GetById(id.Value);
            return domain;
        }
        public async Task<Domain> Create(Domain domain)
        {
            await Repository.Insert(domain);
            return domain;
        }

        public async Task<Domain> Update(Domain domain)
        {
            await Repository.Update(domain);
            return domain;
        }

        public async Task Delete(Domain domain)
        {
            await Repository.Delete(domain);
        }
        public async Task<Domain> GetByName(string name)
        {
            return await Repository.GetByName(name);
        }
    }
}
