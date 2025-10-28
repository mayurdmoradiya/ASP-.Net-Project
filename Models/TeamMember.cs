using System.ComponentModel.DataAnnotations;

namespace Project.Models;

public class TeamMember
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public TeamRole Role { get; set; } = TeamRole.Developer;
    
    // Foreign key
    [Required]
    public int ProjectId { get; set; }
    
    // Navigation property
    public Project? Project { get; set; }
}

public enum TeamRole
{
    ProjectManager,
    Developer,
    Designer,
    Tester,
    Analyst
}
