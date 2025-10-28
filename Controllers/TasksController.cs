using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using TaskStatus = Project.Models.TaskStatus;

namespace Project.Controllers;

public class TasksController : Controller
{
    private readonly ApplicationDbContext _context;

    public TasksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Tasks
    public async Task<IActionResult> Index(int? projectId)
    {
        var tasks = _context.Tasks.Include(t => t.Project).AsQueryable();
        
        if (projectId.HasValue)
        {
            tasks = tasks.Where(t => t.ProjectId == projectId.Value);
            ViewBag.ProjectId = projectId;
        }
        
        return View(await tasks.ToListAsync());
    }

    // GET: Tasks/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // GET: Tasks/Create
    public IActionResult Create(int? projectId)
    {
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", projectId);
        
        var task = new ProjectTask();
        if (projectId.HasValue)
        {
            task.ProjectId = projectId.Value;
        }
        
        return View(task);
    }

    // POST: Tasks/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Description,Priority,Status,DueDate,AssignedTo,ProjectId")] ProjectTask task)
    {
        if (ModelState.IsValid)
        {
            task.CreatedAt = DateTime.Now;
            _context.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
        }
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
        return View(task);
    }

    // GET: Tasks/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
        return View(task);
    }

    // POST: Tasks/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Priority,Status,DueDate,AssignedTo,CreatedAt,CompletedAt,ProjectId")] ProjectTask task)
    {
        if (id != task.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (task.Status == TaskStatus.Completed && task.CompletedAt == null)
                {
                    task.CompletedAt = DateTime.Now;
                }
                
                _context.Update(task);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(task.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
        }
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
        return View(task);
    }

    // GET: Tasks/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // POST: Tasks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // POST: Tasks/UpdateStatus
    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id, TaskStatus status)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        task.Status = status;
        if (status == TaskStatus.Completed && task.CompletedAt == null)
        {
            task.CompletedAt = DateTime.Now;
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    private bool TaskExists(int id)
    {
        return _context.Tasks.Any(e => e.Id == id);
    }
}
