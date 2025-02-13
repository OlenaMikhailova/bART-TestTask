using bART_TestTask.Core.DTOs;
using bART_TestTask.Core.Entities;
using bART_TestTask.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bART_TestTask.Core.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IContactRepository _contactRepository;

        public IncidentService(IIncidentRepository incidentRepository, IAccountRepository accountRepository, IContactRepository contactRepository)
        {
            _incidentRepository = incidentRepository;
            _accountRepository = accountRepository;
            _contactRepository = contactRepository;
        }

        public async Task<IncidentDTO> CreateIncidentAsync(IncidentRequestDTO requestDto)
        {
            if (requestDto == null)
            {
                throw new ArgumentNullException(nameof(requestDto), "Request body cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(requestDto.AccountName))
            {
                throw new ArgumentException("Account name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(requestDto.ContactEmail))
            {
                throw new ArgumentException("Contact email cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(requestDto.IncidentDescription))
            {
                throw new ArgumentException("Incident description cannot be empty.");
            }

            var account = await _accountRepository.GetByNameAsync(requestDto.AccountName);
            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            var contact = await _contactRepository.GetByEmailAsync(requestDto.ContactEmail);

            if (contact != null)
            {
                if (requestDto.ContactFirstName != null)
                {
                    contact.FirstName = requestDto.ContactFirstName;
                }

                if (requestDto.ContactLastName != null)
                {
                    contact.LastName = requestDto.ContactLastName;
                }


                if (contact.Account == null || contact.Account.Name != account.Name)
                {
                    contact.Account = account;
                }

                await _contactRepository.UpdateAsync(contact);
            }
            else
            {
                contact = new Contact
                {
                    FirstName = requestDto.ContactFirstName,
                    LastName = requestDto.ContactLastName,
                    Email = requestDto.ContactEmail,
                    Account = account
                };

                await _contactRepository.CreateAsync(contact);
            }

            var incident = new Incident
            {
                IncidentName = $"Incident-{Guid.NewGuid().ToString().Substring(0, 8)}", 
                Description = requestDto.IncidentDescription,
                Account = account
            };

            await _incidentRepository.CreateAsync(incident);

            return new IncidentDTO
            {
                IncidentName = incident.IncidentName,
                Description = incident.Description,
                AccountName = account.Name
            };
        }
    }
}
