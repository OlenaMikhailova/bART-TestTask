using bART_TestTask.Core.DTOs;
using bART_TestTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART_TestTask.Core.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDTO> GetAccountByNameAsync(string name);
        Task<AccountDTO> CreateAccountAsync(AccountDTO accountDto);

    }
}
