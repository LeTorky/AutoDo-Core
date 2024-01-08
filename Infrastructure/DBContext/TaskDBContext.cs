using Microsoft.EntityFrameworkCore;

public class TaskDBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=AutoDo;Integrated Security=SSPI; TrustServerCertificate=True;");
            }
        }
}
