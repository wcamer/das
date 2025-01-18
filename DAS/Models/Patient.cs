


using System.ComponentModel.DataAnnotations;

namespace DAS.Models;

public class Patient
{
    public int PatientId { get; set; }

    [Required]
    public int ProfileId { get; set; }  // this is a foreign key to a specific profile

    public int? PatientGroupId { get; set; } // this is here if a particular feature is built


    [RegularExpression(@"^[a-zA-Z\s]*$",
    ErrorMessage = "Please enter full name that is at least 2 letters long and no more than 40 letters total")]
    [Required]
    [StringLength(40, MinimumLength = 2)]
    public required string FirstName { get; set; } = string.Empty;

    [RegularExpression(@"^[a-zA-Z\s]*$",
    ErrorMessage = "Please enter full name that is at least 2 letters long and no more than 40 letters total")]
    [Required]
    [StringLength(40, MinimumLength = 2)]
    public required string LastName { get; set; } = string.Empty;

    [RegularExpression(@"^[a-zA-Z\s]*$",
    ErrorMessage = "Please enter the insurance company name that is no more than 40 letters total")]
    [StringLength(40, MinimumLength = 0)]
    public string? InsuranceCompany { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9\s]*$",
    ErrorMessage = "Please enter insurance policy number that is at least 1 character long and no more than 10 characters total")]
    [StringLength(10, MinimumLength = 0)]
    public string? InsurancePolicyNumber { get; set; }






}