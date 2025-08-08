using Ticketapi.Data;
using ticketapi.Services;
using static ticketapi.DTOs.DTO;
using Ticketapi;

namespace ticketapi.Modules
{
    public class TicketModule : IModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<TicketService>();
        }
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            // Add a new ticket to an existing project
            endpoints.MapPost("v1/projects/{projectId}/tickets", async (int projectId, CreateTicketDto dto, TicketService service) =>
            {
                var ticket = await service.AddTicketToProject(projectId, dto);
                return ticket != null ? Results.Ok(ticket) : Results.NotFound("Project or User not found.");
            });

            // Assign ticket to existing user
            endpoints.MapPost("v1/tickets/{ticketId}/assign-user/{userId}", async (int ticketId, int userId, TicketService service) =>
            {
                var result = await service.AssignTicketToUser(ticketId, userId);
                return result ? Results.Ok("User assigned to ticket.") : Results.NotFound("Ticket or User not found.");
            });

            // List all assigned tickets (for showing to-do tickets)
            endpoints.MapGet("v1/tickets/assigned", async (TicketService service) =>
            {
                var tickets = await service.ListAssignedTickets();
                return Results.Ok(tickets);
            });
        }
    }
}
