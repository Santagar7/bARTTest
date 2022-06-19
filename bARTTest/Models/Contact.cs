using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace bARTTest; 

public class Contact {
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Key]
    [EmailAddress]
    public string Email { get; set; }
}