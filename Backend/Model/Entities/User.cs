using System.ComponentModel.DataAnnotations;
using Model.Enums;

namespace Model.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(60)] public string Email { get; set; } = string.Empty;
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;
    [MaxLength(255)]
    public string UserName { get; set; } = string.Empty;
    [MaxLength(255)]
    public string FullName { get; set; } = string.Empty;
    [MaxLength(255)]
    public string PhoneNumber { get; set; } = string.Empty;
    [MaxLength(255)]
    public string ProfileUrl { get; set; } = string.Empty;

    public bool IsDisabled { get; set; } = false;
    public Role Role { get; set; } = Role.Member;
    public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
}