using Microsoft.EntityFrameworkCore;
using Ticketapi.Data;
using Ticketapi.Models;
using static ticketapi.DTOs.DTO;

namespace ticketapi.Services
{
    public class TicketService
    {
        private readonly TicketDbContext _db;
        public TicketService(TicketDbContext db) => _db = db;

        public async Task<Ticket?> AddTicketToProject(int projectId, CreateTicketDto dto)
        {
            var projectExists = await _db.Projects.AnyAsync(p => p.Id == projectId);
            var userExists = await _db.Users.AnyAsync(u => u.Id == dto.UserId);

            if (!projectExists || !userExists)
                return null;

            var ticket = new Ticket
            {
                Description = dto.Description,
                ProjectId = projectId,
                UserId = dto.UserId
            };

            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();
            return ticket;
        }

        public async Task<bool> AssignTicketToUser(int ticketId, int userId)
        {
            var ticket = await _db.Tickets.FindAsync(ticketId);
            var userExists = await _db.Users.AnyAsync(u => u.Id == userId);

            if (ticket == null || !userExists)
                return false;

            ticket.UserId = userId;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Ticket>> ListAssignedTickets()
        {
            return await _db.Tickets
                .Where(t => t.UserId != 0)
                .ToListAsync();
        }
    }
}
