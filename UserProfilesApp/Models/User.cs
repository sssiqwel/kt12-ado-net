using System.ComponentModel.DataAnnotations;

namespace UserProfilesApp.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public UserProfile Profile { get; set; } = null!;
}

