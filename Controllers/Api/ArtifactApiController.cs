using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ArtifactApiController : ControllerBase
{
    private readonly GalleryContext _context;
    public ArtifactApiController(GalleryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string status = null)
    {
        var artifacts = _context.Artifacts.Include(a => a.Comments).AsQueryable();
        if (!string.IsNullOrEmpty(status))
        {
            artifacts = artifacts.Where(a => a.Status == status);
        }
        return Ok(await artifacts.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var artifact = await _context.Artifacts.Include(a => a.Comments).FirstOrDefaultAsync(a => a.Id == id);
        if (artifact == null) return NotFound();
        return Ok(artifact);
    }
}
