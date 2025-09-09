using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CommentApiController : ControllerBase
{
    private readonly GalleryContext _context;
    public CommentApiController(GalleryContext context)
    {
        _context = context;
    }

    [HttpGet("artifact/{artifactId}")]
    public async Task<IActionResult> GetByArtifact(int artifactId)
    {
        var comments = await _context.Comments.Where(c => c.ArtifactId == artifactId).OrderByDescending(c => c.Date).ToListAsync();
        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Comment comment)
    {
        if (string.IsNullOrWhiteSpace(comment.Author) || string.IsNullOrWhiteSpace(comment.Text))
        {
            return BadRequest("Author and Text are required");
        }
        comment.Date = DateTime.Now;
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        var comments = await _context.Comments.Where(c => c.ArtifactId == comment.ArtifactId).OrderByDescending(c => c.Date).ToListAsync();
        return Ok(comments);
    }
}
