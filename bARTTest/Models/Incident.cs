using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace bARTTest; 

public class Incident {
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? Name { get; set; }
    
    public string Description { get; set; }

    public List<Account> Accounts { get; set; } = new List<Account>();
}