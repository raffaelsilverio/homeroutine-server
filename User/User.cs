using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public Guid Id { get; set;} = Guid.NewGuid();
    [Required]
    [MinLength(3), MaxLength(15)]
    public required string Username { get; set;}
    [Required]
    [EmailAddress]
    public required string Email { get; set;}
    [Required]
    [MinLength(8), MaxLength(20)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
    public required string PasswordHash { get; set;}
    public string? Idiom { get; set; }
}