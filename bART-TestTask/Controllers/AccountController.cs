using bART_TestTask.Core.DTOs;
using bART_TestTask.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bART_TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAccountByName(string name)
        {
            try
            {
                var accountDto = await _accountService.GetAccountByNameAsync(name);
                return Ok(accountDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDTO accountDto)
        {
            try
            {
                var createdAccount = await _accountService.CreateAccountAsync(accountDto);
                return CreatedAtAction(nameof(GetAccountByName), new { name = createdAccount.Name }, createdAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
