


using System.ComponentModel.DataAnnotations;

namespace DAS.Models;

public class Appointment
{

    public int AppointmentId { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required]
    public int AppointSetterId { get; set; }

    [RegularExpression(@"^[a-zA-Z\s]*$",
    ErrorMessage = "Please enter full name that is at least 2 letters long and no more than 40 letters total")]
    [Required]
    [StringLength(40, MinimumLength = 2)]
    public required string Name { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public required DateTime Date { get; set; }

    public required string Time { get; set; }

    public required string Status { get; set; } = "Pending";





}