using System.ComponentModel.DataAnnotations;


namespace DAS.Models;

public class DoctorNote
{
    public required int DoctorNoteId { get; set; }

    [DataType(DataType.Date)]
    public required DateTime Date { get; set; }

    public required int PatientId { get; set; }

    public required int DoctorId { get; set; }

    [RegularExpression(@"^[a-zA-z]*$",
    ErrorMessage = "Patient name must be at least 2 letters but no more than 40 letters long")]
    [StringLength(40, MinimumLength = 2)]
    [Required]
    public required string PatientName { get; set; }

    [RegularExpression(@"^[a-zA-z]*$",
    ErrorMessage = "The Doctor name must be at least 2 letters but no more than 40 letters long")]
    [StringLength(40, MinimumLength = 2)]
    [Required]
    public required string DoctorName { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9\s]*$",
    ErrorMessage = "Note contents must only contain letters and numbers, have a min length of 2, and a max length of 1000")]
    [Required]
    [StringLength(1000, MinimumLength = 2)]
    public required string Note { get; set; }












}