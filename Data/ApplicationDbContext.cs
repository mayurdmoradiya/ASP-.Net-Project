using Microsoft.EntityFrameworkCore;
using Project.Models;
using TaskStatus = Project.Models.TaskStatus;

namespace Project.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Models.Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Models.Project>()
            .HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Models.Project>()
            .HasMany(p => p.TeamMembers)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed some initial data
        modelBuilder.Entity<Models.Project>().HasData(
            new Models.Project
            {
                Id = 1,
                Name = "Sample Project",
                Description = "This is a sample project to get you started",
                StartDate = new DateTime(2025, 1, 1),
                Status = ProjectStatus.Planning,
                CreatedAt = new DateTime(2025, 1, 1)
            }
        );

        modelBuilder.Entity<ProjectTask>().HasData(
            new ProjectTask
            {
                Id = 1,
                Title = "Setup project structure",
                Description = "Initialize the project with basic structure",
                Priority = TaskPriority.High,
                Status = TaskStatus.Completed,
                ProjectId = 1,
                CreatedAt = new DateTime(2025, 1, 1),
                CompletedAt = new DateTime(2025, 1, 1)
            },
            new ProjectTask
            {
                Id = 2,
                Title = "Design database schema",
                Description = "Create entity models and relationships",
                Priority = TaskPriority.High,
                Status = TaskStatus.InProgress,
                ProjectId = 1,
                DueDate = new DateTime(2025, 1, 10),
                CreatedAt = new DateTime(2025, 1, 1)
            }
        );
    }
}
