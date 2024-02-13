using Library.Entities;
using LibraryCore.Interfaces;

namespace LibraryAPI.Services
{
    public class LoanService
    {
        private readonly ILoanRepository Repository;

        public LoanService(ILoanRepository repo)
        {
            Repository = repo;
        }

        public async Task<List<Loan>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task<Loan> GetById(int? id)
        {
            var loan = await Repository.GetById(id.Value);
            return loan;
        }
        public async Task<Loan> Create(Loan loan)
        {
            await Repository.Insert(loan);
            return loan;
        }

        public async Task<Loan> Update(Loan loan)
        {
            await Repository.Update(loan);
            return loan;
        }

        public async Task Delete(Loan loan)
        {
            await Repository.Delete(loan);
        }
        public async Task<Loan> GetSingle(Loan loan)
        {
            return await Repository.GetSingle(loan);
        }

        public async Task<bool> Exists(int id)
        {
            return await Repository.Exist(id);
        }

        public async Task<List<Loan>> GetCurrentLoans()
        {
            return await Repository.GetCurrentLoans();
        }

        public async Task<List<Loan>> GetFinishedLoans()
        {
            return await Repository.GetFinishedLoans();
        }
    }
}
