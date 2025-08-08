using Microsoft.EntityFrameworkCore;
using Ticketapi.Data;
using Ticketapi.Models;
using static ticketapi.DTOs.DTO;

namespace ticketapi.Services
{
    public class TicketTaskService
    {
        private readonly TicketDbContext _db;
        public TicketTaskService(TicketDbContext db) => _db = db;

        public async Task<TicketTask?> AddSubtaskToTicket(int ticketId, CreateTicketTaskDto dto)
        {
            var ticketExists = await _db.Tickets.AnyAsync(t => t.Id == ticketId);
            var userExists = await _db.Users.AnyAsync(u => u.Id == dto.UserId);

            if (!ticketExists || !userExists)
                return null;

            var task = new TicketTask
            {
                Description = dto.Description,
                TicketId = ticketId,
                UserId = dto.UserId
            };

            _db.TicketTasks.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<bool> AssignSubtaskToUser(int taskId, int userId)
        {
            var task = await _db.TicketTasks.FindAsync(taskId);
            var userExists = await _db.Users.AnyAsync(u => u.Id == userId);

            if (task == null || !userExists)
                return false;

            task.UserId = userId;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
