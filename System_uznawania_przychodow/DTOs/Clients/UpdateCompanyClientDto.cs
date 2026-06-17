using System.ComponentModel.DataAnnotations;

namespace System_uznawania_przychodow.DTOs.Clients;

public class UpdateCompanyClientDto
{
    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; } = string.Empty;
    
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
}