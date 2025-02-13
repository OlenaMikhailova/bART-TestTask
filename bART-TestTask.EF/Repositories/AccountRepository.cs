using bART_TestTask.Core.Entities;
using bART_TestTask.Core.Interfaces;
using bART_TestTask.EF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART_TestTask.EF.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetByNameAsync(string name)
        {
            return await _context.Accounts
                .Include(a => a.Contacts)
                .Include(a => a.Incidents)
                .FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task<Account> CreateAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
