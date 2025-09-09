using System.ComponentModel.DataAnnotations;

public class Artifact
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string History { get; set; }
    [Required]
    public string Status { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
