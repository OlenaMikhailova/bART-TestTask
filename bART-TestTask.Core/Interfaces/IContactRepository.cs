using bART_TestTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART_TestTask.Core.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact?> GetByEmailAsync(string email);
        Task<Contact> CreateAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task SaveChangesAsync();
    }
}
