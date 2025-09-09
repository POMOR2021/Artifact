using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Login { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    public string Role { get; set; }
}
