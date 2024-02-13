using Library.Entities;
using LibraryCore.Interfaces;

namespace LibraryAPI.Services
{
    public class ReaderService
    {
        private readonly IReaderRepository Repository;

        public ReaderService(IReaderRepository repo)
        {
            Repository = repo;
        }

        public async Task<List<Reader>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task<Reader> GetById(int? id)
        {
            var reader = await Repository.GetById(id.Value);
            return reader;
        }
        public async Task<Reader> Create(Reader reader)
        {
            await Repository.Insert(reader);
            return reader;
        }

        public async Task<Reader> Update(Reader reader)
        {
            await Repository.Update(reader);
            return reader;
        }

        public async Task Delete(Reader reader)
        {
            await Repository.Delete(reader);
        }
        public async Task<Reader> GetSingle(Reader reader)
        {
            return await Repository.GetSingle(reader.Mail);
        }
        public async Task<Reader> GetSingle(string mail)
        {
            return await Repository.GetSingle(mail);
        }
    }
}
