using Library.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.Interfaces
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAll();
        Task<Loan> GetById(int id);
        Task<Loan> GetSingle(Loan loan);
        Task<bool> Exist(int id);
        Task Insert(Loan loan);
        Task Update(Loan loan);
        Task Delete(Loan loan);
        Task<List<Loan>> GetCurrentLoans();
        Task<List<Loan>> GetFinishedLoans();
    }
}
