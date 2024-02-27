using TaskMaster.Entities.Enums;
using TaskMaster.Entities.Models;

namespace TaskMaster.Entities.DTOs;

public record TaskDto(Guid Id, string Title, string Description, Priority Priority, Status Status, DateTime DueDate, IEnumerable<Tag> Tags);