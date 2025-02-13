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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IContactRepository _contactRepository;

        public AccountService(IAccountRepository accountRepository, IContactRepository contactRepository)
        {
            _accountRepository = accountRepository;
            _contactRepository = contactRepository;
        }


        public async Task<AccountDTO> GetAccountByNameAsync(string name)
        {
            var account = await _accountRepository.GetByNameAsync(name);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            return new AccountDTO
            {
                Name = account.Name,
                Contacts = account.Contacts.Select(c => new ContactDTO
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email
                }).ToList(),
                Incidents = account.Incidents.Select(i => new IncidentDTO
                {
                    IncidentName = i.IncidentName,
                    Description = i.Description,
                    AccountName = account.Name
                }).ToList()
            };
        }

        public async Task<AccountDTO> CreateAccountAsync(AccountDTO accountDto)
        {
            try
            {
                if (accountDto == null)
                {
                    throw new ArgumentNullException(nameof(accountDto), "Request body cannot be null.");
                }

                if (string.IsNullOrWhiteSpace(accountDto.Name))
                {
                    throw new ArgumentException("Account name cannot be empty.");
                }

                if (accountDto.Contacts == null || !accountDto.Contacts.Any())
                {
                    throw new ArgumentException("Account must have at least one contact.");
                }

                var existingAccount = await _accountRepository.GetByNameAsync(accountDto.Name);
                if (existingAccount != null)
                {
                    throw new ArgumentException("Account with this name already exists.");
                }

                var uniqueContacts = accountDto.Contacts.DistinctBy(c => c.Email).ToList();

                var contactsToAdd = new List<Contact>();

                foreach (var contactDto in uniqueContacts)
                {
                    if (string.IsNullOrWhiteSpace(contactDto.Email))
                    {
                        throw new ArgumentException("Contact email cannot be empty.");
                    }

                    var existingContact = await _contactRepository.GetByEmailAsync(contactDto.Email);
                    if (existingContact != null)
                    {
                        if (existingContact.Account == null || existingContact.Account.Name != accountDto.Name)
                        {
                            existingContact.Account = existingAccount;
                            await _contactRepository.UpdateAsync(existingContact);
                        }
                        contactsToAdd.Add(existingContact);
                    }
                    else
                    {
                        contactsToAdd.Add(new Contact
                        {
                            FirstName = contactDto.FirstName,
                            LastName = contactDto.LastName,
                            Email = contactDto.Email
                        });
                    }
                }

                var account = new Account
                {
                    Name = accountDto.Name,
                    Contacts = contactsToAdd
                };

                account = await _accountRepository.CreateAsync(account);

                return new AccountDTO
                {
                    Name = account.Name,
                    Contacts = account.Contacts.Select(c => new ContactDTO
                    {
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email
                    }).ToList(),
                    Incidents = new List<IncidentDTO>()
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while creating account: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}
