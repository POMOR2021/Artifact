using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class ArtifactController : Controller
{
    private readonly GalleryContext _context;
    public ArtifactController(GalleryContext context)
    {
        _context = context;
    }

    [ServiceFilter(typeof(ActionLoggingFilter))]
    [ServiceFilter(typeof(ArtifactAccessFilter))]
    public async Task<IActionResult> Index(string status = null)
    {
        var artifacts = _context.Artifacts.Include(a => a.Comments).AsQueryable();
        if (!string.IsNullOrEmpty(status))
        {
            artifacts = artifacts.Where(a => a.Status == status);
        }
        return View(await artifacts.ToListAsync());
    }

    [ServiceFilter(typeof(ActionLoggingFilter))]
    public async Task<IActionResult> Details(int id)
    {
        var artifact = await _context.Artifacts.Include(a => a.Comments).FirstOrDefaultAsync(a => a.Id == id);
        if (artifact == null) return NotFound();
        return View(artifact);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(string title, string description, string history, string status)
    {
        var artifact = new Artifact
        {
            Title = title,
            Description = description,
            History = history,
            Status = status
        };
        _context.Artifacts.Add(artifact);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeStatus(int id, string newStatus)
    {
        var artifact = await _context.Artifacts.FindAsync(id);
        if (artifact == null)
            return NotFound();
        artifact.Status = newStatus;
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RandomizeStatuses()
    {
        var statuses = new[] { "в экспозиции", "на реставрации", "в хранилище" };
        var artifacts = _context.Artifacts.ToList();
        var rnd = new System.Random();
        foreach (var artifact in artifacts)
        {
            artifact.Status = statuses[rnd.Next(statuses.Length)];
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
