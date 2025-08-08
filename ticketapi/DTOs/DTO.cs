namespace ticketapi.DTOs
{
    public class DTO
    {
        // CreateTicketDto.cs
        public record CreateTicketDto
        {
            public required string Description { get; init; }
            public required int UserId { get; init; }
        }

        // TicketDto.cs (for returning ticket info in responses)
        public record TicketDto
        {
            public int Id { get; init; }
            public string? Description { get; init; }
            public int ProjectId { get; init; }
            public int UserId { get; init; }
        }

        // CreateTicketTaskDto.cs
        public record CreateTicketTaskDto
        {
            public required string Description { get; init; }
            public required int UserId { get; init; }
        }

        // TicketTaskDto.cs (for responses)
        public record TicketTaskDto
        {
            public int Id { get; init; }
            public string? Description { get; init; }
            public int TicketId { get; init; }
            public int UserId { get; init; }
        }

        // ProjectDto.cs (for returning project info)
        public record ProjectDto
        {
            public int Id { get; init; }
            public string? Name { get; init; }
            public List<UserDto> Users { get; init; } = new();
        }

        // UserDto.cs (for user info in responses)
        public record UserDto
        {
            public int Id { get; init; }
            public string? Name { get; init; }
        }

    }
}
