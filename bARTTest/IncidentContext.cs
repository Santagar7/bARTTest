using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace bARTTest; 

[NotMapped]
public class IncidentContext {
    public string AccountName { get; set; }
    public string IncidentDecription { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    
}