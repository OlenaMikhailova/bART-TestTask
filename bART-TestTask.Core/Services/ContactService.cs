using bART_TestTask.Core.DTOs;
using bART_TestTask.Core.Entities;
using bART_TestTask.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART_TestTask.Core.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IAccountRepository _accountRepository;

        public ContactService(IContactRepository contactRepository, IAccountRepository accountRepository)
        {
            _contactRepository = contactRepository;
            _accountRepository = accountRepository;
        }

        public async Task<ContactDTO> CreateOrUpdateContactAsync(string accountName, ContactDTO contactDto)
        {
            var account = await _accountRepository.GetByNameAsync(accountName);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            var contact = await _contactRepository.GetByEmailAsync(contactDto.Email);
            if (contact == null)
            {
                contact = new Contact
                {
                    FirstName = contactDto.FirstName,
                    LastName = contactDto.LastName,
                    Email = contactDto.Email,
                    AccountId = account.Id
                };
                await _contactRepository.CreateAsync(contact);
            }
            else
            {
                contact.FirstName = contactDto.FirstName;
                contact.LastName = contactDto.LastName;
                await _contactRepository.UpdateAsync(contact);
            }

            return new ContactDTO
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email
            };
        }
    }
}
