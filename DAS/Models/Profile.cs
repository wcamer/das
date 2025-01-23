using System.ComponentModel.DataAnnotations;


namespace DAS.Models;

public class Profile
{

    public int ProfileId { get; set; }

    public string? Type { get; set; } = "Patient"; //defaults to Patient

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

    [RegularExpression(@"^[0-9]+$")]
    [Required]
    [StringLength(10, MinimumLength = 10,
    ErrorMessage = "Please enter your 10 digit telephone with only the numbers and no dashes")]
    public required string Telephone { get; set; }


    [RegularExpression(@"^[a-zA-Z0-9\s]*$",
    ErrorMessage = "Address must only contain letters and numbers, have a min length of 2, and a max length of 50")]
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public required string Address { get; set; }


    [RegularExpression(@"^[a-zA-Z0-9\s]*$",
    ErrorMessage = "City name must contain letters or numbers, min length of 2 and max length of 20")]
    [Required]
    [StringLength(20, MinimumLength = 2)]
    public required string City { get; set; }


    [RegularExpression(@"^[a-zA-Z0-9\s]*$",
    ErrorMessage = "State name must contain letters and/or numbers with a min length of 2 and max length of 20")]
    [Required]
    [StringLength(20, MinimumLength = 2)]
    public required string State { get; set; }


    [Required]
    [StringLength(15, MinimumLength = 2,
    ErrorMessage = "ZipCode code must have at least a length of 2 but no more than 15")]
    public required string ZipCode { get; set; }



    public string? Email { get; set; }

    public string? Password { get; set; }

    // public string HashedPassword { get; set; } = string.Empty;

}