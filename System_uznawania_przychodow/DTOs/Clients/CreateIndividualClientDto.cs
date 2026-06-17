using System.ComponentModel.DataAnnotations;

namespace System_uznawania_przychodow.DTOs.Clients;

public class CreateIndividualClientDto
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(9)]
    [MinLength(9)]
    public string Phone { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(11)]
    [MinLength(11)]
    public string Pesel { get; set; } = string.Empty;
}