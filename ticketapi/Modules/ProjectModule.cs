using Ticketapi.Data;
using Ticketapi.Services;

namespace Ticketapi.Modules;
public class ProjectModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("v1/projects", async (ProjectService service, TicketDbContext db) =>
        {
            return Results.Json(await service.GetProjects(db));
        });
        endpoints.MapGet("v1/projects/users", async (ProjectService service, TicketDbContext db) =>
        {
            return Results.Json(await service.GetProjectsAlongWithAssignedUsers(db));
        });
        // Assign project to user
        endpoints.MapPost("v1/projects/{projectId}/assign-user/{userId}", async (int projectId, int userId, ProjectService service) =>
        {
            var result = await service.AssignProjectToUser(projectId, userId);
            return result ? Results.Ok("User assigned to project.") : Results.NotFound("Project or User not found.");
        });
    }

    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ProjectService>();
    }
}