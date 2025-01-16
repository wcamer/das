using System.ComponentModel.DataAnnotations;
// using CarServiceScheduler.Components.Pages;

namespace DAS.Models;

public class ServiceProviders
{

    public required int ServiceProvidersId { get; set; }

    [RegularExpression(@"^[a-zA-z]*$",
    ErrorMessage = "First name must be at least 2 letters but no more than 20 letters long")]
    [StringLength(20, MinimumLength = 2)]
    [Required]
    public required string FirstName { get; set; }

    [RegularExpression(@"^[a-zA-z]*$")]
    [Required]
    [StringLength(20, MinimumLength = 2,
    ErrorMessage = "Last name must be at least 2 letters long but no more than 20 letters")]
    public required string LastName { get; set; }


    public string? Email { get; set; }

    public string? Password { get; set; }

}