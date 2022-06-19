using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bARTTest.Controllers; 

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase {
    
    private readonly ApplicationDbContext _db;

    public AccountController(ApplicationDbContext db) {
        _db = db;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(AccountContext accountContext) {
        if (ModelState.IsValid) {
            bool accountExists = false;
            Account account = _db.Accounts.FirstOrDefault(a => a.Username == accountContext.UserName);
            if (account == null) {
                accountExists = false;
                account = new Account() {
                    Username = accountContext.UserName
                };
            } else {
                accountExists = true;
            }
            
            bool contactExists = false;
            Contact contact = await _db.Contacts.FindAsync(accountContext.Email);
            if (contact != null) {
                contactExists = true;
                contact.FirstName = accountContext.FirstName;
                contact.LastName = accountContext.LastName;
            } else {
                contact = new Contact() {
                    FirstName = accountContext.FirstName,
                    LastName = accountContext.LastName,
                    Email = accountContext.Email,
                };
            }
            account.Contacts.Add(contact); 
            if (!accountExists) {
                _db.Accounts.Add(account);
            } else {
                _db.Accounts.Update(account);
            }
            if (contactExists) {
                _db.Contacts.Update(contact);
            } else {
               _db.Contacts.Add(contact);
            }
            await _db.SaveChangesAsync();
            return Ok();
        }
        return BadRequest(ModelState);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get() {
        return Ok(await _db.Accounts.ToListAsync());
    }


}