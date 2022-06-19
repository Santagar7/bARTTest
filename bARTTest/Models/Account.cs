using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace bARTTest; 

public class Account {
    
    [Key]
    public string Username { get; set; }
    
    public List<Contact> Contacts { get; set; } = new List<Contact>();
}