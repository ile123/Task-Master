using System.ComponentModel.DataAnnotations;
using Model.Enums;

namespace Model.Entities;

public class Task
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(60)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(150)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(150)]
    public string Tags { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Low;
    public Status Status { get; set; } = Status.Todo;
    public DateTime DueDate { get; set; } = DateTime.Today;
    public bool IsDisabled { get; set; } = false;
    public User? User { get; set; }
}