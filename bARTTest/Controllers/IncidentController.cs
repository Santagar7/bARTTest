using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bARTTest.Controllers; 

[ApiController]
[Route("[controller]")]
public class IncidentController : ControllerBase {
    
    private readonly ApplicationDbContext _db;
    
    public IncidentController(ApplicationDbContext db) {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult> Post(IncidentContext incident) {
        if (ModelState.IsValid) {
            Incident newIncident = new Incident {
                Description = incident.IncidentDecription
            };
            Account account = await _db.Accounts.FindAsync(incident.AccountName);
            if (account == null) {
                return NotFound("Account not found");
            }

            bool contactExists = false;
            Contact contact = await _db.Contacts.FindAsync(incident.Email);
            if (contact != null) {
                contactExists = true;
                contact.FirstName = incident.FirstName;
                contact.LastName = incident.LastName;
            } else {
                contact = new Contact() {
                    FirstName = incident.FirstName,
                    LastName = incident.LastName,
                    Email = incident.Email,
                };
            }
            
            newIncident.Accounts.Add(account);
            _db.Incidents.Add(newIncident);
            account.Contacts.Add(contact);
            _db.Accounts.Update(account);
            if (!contactExists) {
                _db.Contacts.Add(contact);
            } else {
                _db.Contacts.Update(contact);
            }
            await _db.SaveChangesAsync();
            return Ok();
        } else {
            return BadRequest(ModelState);
        }
    }

    [HttpGet]
    public async Task<ActionResult> Get() {
        List<Incident> incidents = await _db.Incidents.ToListAsync();
        return Ok(incidents);
    }

}