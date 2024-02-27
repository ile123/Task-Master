using System.ComponentModel.DataAnnotations;
using TaskMaster.Entities.Enums;

namespace TaskMaster.Entities.Models;

public class Task
{
    public Guid Id { get; set; }
    [MaxLength(60)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(150)]
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Low;
    public Status Status { get; set; } = Status.Todo;
    public DateTime DueDate { get; set; } = DateTime.Today;
    public bool IsDisabled { get; set; } = false;
    public User? User { get; set; }
    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
}