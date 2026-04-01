using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserProfilesApp.Models;

public class UserProfile
{
    [Key]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    public DateOnly? BirthDate { get; set; }

    [MaxLength(1000)]
    public string? Bio { get; set; }

    public User User { get; set; } = null!;
}

