using System.ComponentModel.DataAnnotations;

namespace System_uznawania_przychodow.DTOs.Contracts;

public class CreateContractDto
{
    [Required]
    public int ClientId { get; set; }
    [Required]
    public int SoftwareId { get; set; }
    [Required]
    public DateOnly StartDate { get; set; }
    [Required]
    public DateOnly EndDate { get; set; }
    [Range(0, 3)]
    public int AdditionalSupportYears { get; set; }
}