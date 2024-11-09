using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Enums;

namespace Model.Entities;

public class Assignment
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
    public string DueDate { get; set; } = string.Empty;
    public bool IsDisabled { get; set; } = false;
    public Guid UserId { get; set; }
    public User User { get; set; }
}