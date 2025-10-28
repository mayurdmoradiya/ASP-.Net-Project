using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using TaskStatus = Project.Models.TaskStatus;

namespace Project.Controllers;

public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new DashboardViewModel
        {
            TotalProjects = await _context.Projects.CountAsync(),
            ActiveProjects = await _context.Projects.CountAsync(p => p.Status == ProjectStatus.InProgress),
            TotalTasks = await _context.Tasks.CountAsync(),
            CompletedTasks = await _context.Tasks.CountAsync(t => t.Status == TaskStatus.Completed),
            PendingTasks = await _context.Tasks.CountAsync(t => t.Status == TaskStatus.Todo),
            InProgressTasks = await _context.Tasks.CountAsync(t => t.Status == TaskStatus.InProgress),
            OverdueTasks = await _context.Tasks.CountAsync(t => t.DueDate < DateTime.Now && t.Status != TaskStatus.Completed),
            RecentProjects = await _context.Projects
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .Include(p => p.Tasks)
                .ToListAsync(),
            UpcomingTasks = await _context.Tasks
                .Where(t => t.DueDate != null && t.Status != TaskStatus.Completed)
                .OrderBy(t => t.DueDate)
                .Take(10)
                .Include(t => t.Project)
                .ToListAsync(),
            TasksByPriority = await _context.Tasks
                .Where(t => t.Status != TaskStatus.Completed)
                .GroupBy(t => t.Priority)
                .Select(g => new TaskPriorityCount
                {
                    Priority = g.Key,
                    Count = g.Count()
                })
                .ToListAsync()
        };

        return View(viewModel);
    }
}
