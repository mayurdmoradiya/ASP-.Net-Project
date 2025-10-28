using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers;

public class TeamMembersController : Controller
{
    private readonly ApplicationDbContext _context;

    public TeamMembersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: TeamMembers
    public async Task<IActionResult> Index(int? projectId)
    {
        var members = _context.TeamMembers.Include(t => t.Project).AsQueryable();
        
        if (projectId.HasValue)
        {
            members = members.Where(t => t.ProjectId == projectId.Value);
            ViewBag.ProjectId = projectId;
        }
        
        return View(await members.ToListAsync());
    }

    // GET: TeamMembers/Create
    public IActionResult Create(int? projectId)
    {
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", projectId);
        
        var member = new TeamMember();
        if (projectId.HasValue)
        {
            member.ProjectId = projectId.Value;
        }
        
        return View(member);
    }

    // POST: TeamMembers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Email,Role,ProjectId")] TeamMember teamMember)
    {
        if (ModelState.IsValid)
        {
            _context.Add(teamMember);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Projects", new { id = teamMember.ProjectId });
        }
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", teamMember.ProjectId);
        return View(teamMember);
    }

    // GET: TeamMembers/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var teamMember = await _context.TeamMembers.FindAsync(id);
        if (teamMember == null)
        {
            return NotFound();
        }
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", teamMember.ProjectId);
        return View(teamMember);
    }

    // POST: TeamMembers/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Role,ProjectId")] TeamMember teamMember)
    {
        if (id != teamMember.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(teamMember);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamMemberExists(teamMember.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Details", "Projects", new { id = teamMember.ProjectId });
        }
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", teamMember.ProjectId);
        return View(teamMember);
    }

    // GET: TeamMembers/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var teamMember = await _context.TeamMembers
            .Include(t => t.Project)
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (teamMember == null)
        {
            return NotFound();
        }

        return View(teamMember);
    }

    // POST: TeamMembers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var teamMember = await _context.TeamMembers.FindAsync(id);
        int projectId = teamMember?.ProjectId ?? 0;
        
        if (teamMember != null)
        {
            _context.TeamMembers.Remove(teamMember);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Details", "Projects", new { id = projectId });
    }

    private bool TeamMemberExists(int id)
    {
        return _context.TeamMembers.Any(e => e.Id == id);
    }
}
