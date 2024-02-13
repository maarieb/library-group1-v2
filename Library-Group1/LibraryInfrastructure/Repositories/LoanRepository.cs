using Library.Data;
using Library.Entities;
using LibraryCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        public LibraryContext Context { get; }

        public LoanRepository(LibraryContext context)
        {
            Context = context;
        }

        public async Task<List<Loan>> GetAll()
        {
            return await Context.Loans
                .Include(w => w.Reader)
                .ThenInclude(r => r.Address)
                .Include(w => w.Book)
                .ThenInclude(b => b.Writer)
                .Include(w => w.Book.Domain)
                .ToListAsync();
        }

        public async Task<List<Loan>> GetCurrentLoans()
        {
            return await Context.Loans
             .Include(w => w.Reader)
             .ThenInclude(r => r.Address)
             .Include(w => w.Book)
             .ThenInclude(b => b.Writer)
             .Include(w => w.Book.Domain)
             .Where(w => w.EndDate == null)
             .ToListAsync();
        }

        public async Task<List<Loan>> GetFinishedLoans()
        {
            return await Context.Loans
             .Include(w => w.Reader)
             .ThenInclude(r => r.Address)
             .Include(w => w.Book)
             .ThenInclude(b => b.Writer)
             .Include(w => w.Book.Domain)
             .Where(w => w.EndDate != null)
             .ToListAsync();
        }

        public async Task<Loan?> GetById(int id)
        {
            return await Context.Loans
                .Include(w => w.Reader)
                .ThenInclude(r => r.Address)
                .Include(w => w.Book)
                .ThenInclude(b => b.Writer)
                .Include(w => w.Book.Domain)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Loan?> GetSingle(Loan loan)
        {
            return await Context.Loans
                .Include(w => w.Reader)
                .ThenInclude(r => r.Address)
                .Include(w => w.Book)
                .ThenInclude(b => b.Writer)
                .Include(w => w.Book.Domain)
                .FirstOrDefaultAsync(w => w.StartDate.Equals(loan.StartDate) && w.Reader.Equals(loan.Reader) && w.Book.Equals(loan.Book));
        }

        public async Task Insert(Loan loan)
        {
            Context.Loans.Add(loan);
            await Context.SaveChangesAsync();
        }

        public async Task Update(Loan loan)
        {
            Context.Loans.Update(loan);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(Loan loan)
        {
            Context.Loans.Remove(loan);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> Exist(int id)
        {
            return await Context.Loans.AnyAsync(w => w.Id.Equals(id));
        }
    }
}
