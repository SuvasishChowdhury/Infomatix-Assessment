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

        public async Task<List<UserTicketDto>> GetUserTickets()
        {
            return await (from t1 in _db.TicketTasks
                        join t2 in _db.Tickets on t1.TicketId equals t2.Id
                        join t3 in _db.Users on t1.UserId equals t3.Id
                        select new UserTicketDto
                        {
                            User = t3.Name,
                            Description = t2.Description
                        }).ToListAsync();
        }
    }
}
