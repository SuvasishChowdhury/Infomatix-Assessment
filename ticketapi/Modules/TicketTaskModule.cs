using Ticketapi.Data;
using ticketapi.Services;
using static ticketapi.DTOs.DTO;
using Ticketapi;

namespace ticketapi.Modules
{
    public class TicketTaskModule : IModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<TicketTaskService>();
        }

        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            // Add subtask to ticket
            endpoints.MapPost("v1/tickets/{ticketId}/subtasks", async (int ticketId, CreateTicketTaskDto dto, TicketTaskService service) =>
            {
                var task = await service.AddSubtaskToTicket(ticketId, dto);
                return task != null ? Results.Ok(task) : Results.NotFound("Ticket or User not found.");
            });

            // Assign subtask to user
            endpoints.MapPost("v1/subtasks/{taskId}/assign-user/{userId}", async (int taskId, int userId, TicketTaskService service) =>
            {
                var result = await service.AssignSubtaskToUser(taskId, userId);
                return result ? Results.Ok("User assigned to subtask.") : Results.NotFound("Subtask or User not found.");
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
