using System.ComponentModel.DataAnnotations;

namespace Project.Models;

public class ProjectTask
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    [Required]
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    
    [Required]
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    
    public DateTime? DueDate { get; set; }
    
    public string? AssignedTo { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime? CompletedAt { get; set; }
    
    // Foreign key
    [Required]
    public int ProjectId { get; set; }
    
    // Navigation property
    public Project? Project { get; set; }
}

public enum TaskPriority
{
    Low,
    Medium,
    High,
    Critical
}

public enum TaskStatus
{
    Todo,
    InProgress,
    Review,
    Completed,
    Blocked
}
