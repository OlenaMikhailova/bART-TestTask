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
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contact?> GetByEmailAsync(string email)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Contact> CreateAsync(Contact contact)
        {
            var existingContact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == contact.Email);
            if (existingContact != null)
            {
                return existingContact; 
            }

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }


        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
