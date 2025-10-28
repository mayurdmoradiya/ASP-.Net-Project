namespace Project.Models;

public class DashboardViewModel
{
    public int TotalProjects { get; set; }
    public int ActiveProjects { get; set; }
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int PendingTasks { get; set; }
    public int InProgressTasks { get; set; }
    public int OverdueTasks { get; set; }
    public List<Project> RecentProjects { get; set; } = new();
    public List<ProjectTask> UpcomingTasks { get; set; } = new();
    public List<TaskPriorityCount> TasksByPriority { get; set; } = new();
}

public class TaskPriorityCount
{
    public TaskPriority Priority { get; set; }
    public int Count { get; set; }
}
