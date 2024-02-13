using Library.Entities;
using LibraryCore.Interfaces;

namespace LibraryAPI.Services
{
    public class WriterService : IService<Writer>
    {
        private readonly IWriterRepository Repository;

        public WriterService(IWriterRepository repo)
        {
            Repository = repo;
        }

        public async Task<List<Writer>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task<Writer> GetById(int? id)
        {
            var writer = await Repository.GetById(id.Value);
            return writer;
        }
        public async Task<Writer> Create(Writer writer)
        {
            await Repository.Insert(writer);
            return writer;
        }


        // A voir si CRUD complet Auteur
        public async Task<Writer> Update(Writer writer)
        {
            await Repository.Update(writer);
            return writer;
        }

        public async Task Delete(Writer writer)
        {
            await Repository.Delete(writer);
        }
        public async Task<Writer> GetSingle(string lastname, string firstname)
        {
            return await Repository.GetSingle(lastname, firstname);
        }

        public Task<Writer>? GetById(int id)
        {
            var writer = await Repository.GetById(id.Value);
            return writer;
        }

        public Task<Writer> Add(Writer entity)
        {
            throw new NotImplementedException();
        }

        public Task<Writer> GetSingle(Writer entity)
        {
            throw new NotImplementedException();
        }


    }
}
