using System;
using System.ComponentModel.DataAnnotations;

public class Comment
{
    public int Id { get; set; }
    [Required]
    public int ArtifactId { get; set; }
    public Artifact Artifact { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public string Text { get; set; }
    public DateTime Date { get; set; }
}
