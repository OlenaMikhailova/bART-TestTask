using bART_TestTask.Core.DTOs;
using bART_TestTask.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bART_TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("{accountName}")]
        public async Task<IActionResult> CreateOrUpdateContact(string accountName, [FromBody] ContactDTO contactDto)
        {
            try
            {
                var updatedContact = await _contactService.CreateOrUpdateContactAsync(accountName, contactDto);
                return Ok(updatedContact);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
