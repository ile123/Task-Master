using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Entities.Models;

public class Tag
{
    public Guid Id { get; set; }
    [MaxLength(40)]
    public string Name { get; set; } = string.Empty;
    public bool IsDisabled { get; set; } = false;
    public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
}