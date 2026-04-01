using System.ComponentModel.DataAnnotations;

namespace UserProfilesApp.ViewModels;

public class UserUpsertViewModel
{
    public int? Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "Username")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Birth date")]
    [DataType(DataType.Date)]
    public DateOnly? BirthDate { get; set; }

    [MaxLength(1000)]
    public string? Bio { get; set; }
}

