using Microsoft.EntityFrameworkCore;
using Ticketapi.Data;
using Ticketapi.Models;
using static ticketapi.DTOs.DTO;

namespace Ticketapi.Services;

public class ProjectService
{
    private readonly TicketDbContext _db;
    public ProjectService(TicketDbContext db) => _db = db;

    public async Task<bool> AssignProjectToUser(int projectId, int userId)
    {
        var project = await _db.Projects.Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == projectId);
        var user = await _db.Users.FindAsync(userId);

        if (project == null || user == null)
            return false;

        if (!project.Users.Any(u => u.Id == userId))
        {
            project.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        return true;
    }
    public async Task<List<Project>> GetProjects(TicketDbContext db)
    {
        return await db.Projects.ToListAsync();
    }
    public async Task<List<ProjectDto>> GetProjectsAlongWithAssignedUsers(TicketDbContext db)
    {
        return await db.Projects
        .Include(p => p.Users)
        .Select(p => new ProjectDto
        {
            Name = p.Name,
            Users = p.Users.Select(u => new UserDto { Name = u.Name }).ToList()
        })
        .ToListAsync();
    }
    
}