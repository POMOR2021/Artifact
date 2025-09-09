using Microsoft.EntityFrameworkCore;
using System.Linq;

public class GalleryContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Artifact> Artifacts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public GalleryContext(DbContextOptions<GalleryContext> options) : base(options)
    {
    }

    public void Seed()
    {
        if (!Users.Any())
        {
            Users.Add(new User { Login = "admin123", PasswordHash = BCrypt.Net.BCrypt.HashPassword("33445566"), Role = "Admin" });
            Users.Add(new User { Login = "user", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user"), Role = "User" });
            SaveChanges();
        }
        if (!Artifacts.Any())
        {
            var a1 = new Artifact { Title = "Мона Лиза", Description = "Картина Леонардо да Винчи.", History = "Создана в 1503 году.", Status = "в экспозиции" };
            var a2 = new Artifact { Title = "Давид", Description = "Статуя Микеланджело.", History = "Создана в 1504 году.", Status = "на реставрации" };
            var a3 = new Artifact { Title = "Кодекс Лестера", Description = "Рукопись Леонардо да Винчи.", History = "Приобретён Биллом Гейтсом в 1994 году.", Status = "в хранилище" };
            Artifacts.AddRange(a1, a2, a3);
            SaveChanges();
            Comments.Add(new Comment { ArtifactId = a1.Id, Author = "admin123", Text = "Величайший шедевр!", Date = System.DateTime.Now });
            Comments.Add(new Comment { ArtifactId = a2.Id, Author = "user", Text = "Жду окончания реставрации.", Date = System.DateTime.Now });
            SaveChanges();
        }
    }
}
