using bART_TestTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART_TestTask.Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetByNameAsync(string name);
        Task<Account> CreateAsync(Account account);
        Task SaveChangesAsync();
    }
}
