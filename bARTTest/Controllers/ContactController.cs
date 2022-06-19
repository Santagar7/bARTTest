using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bARTTest.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase {
    private readonly ApplicationDbContext _db;

    public ContactController(ApplicationDbContext db) {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult> Upsert(Contact model) {
        if (ModelState.IsValid) {
            var result = await _db.Contacts.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (result == null) {
                _db.Contacts.Add(model);
            } else {
                _db.Contacts.Remove(result);
                _db.Contacts.Update(model);
            }
            await _db.SaveChangesAsync();
            return Ok();
        } else {
            return BadRequest(ModelState);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult> Get() {
        var result = await _db.Contacts.ToListAsync();
        return Ok(result);
    }

}