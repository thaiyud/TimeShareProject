using Microsoft.AspNetCore.Identity;

namespace TimeShareProject.Models;

public partial class ApplicationRole : IdentityRole<Guid>
{
    public string? FullName { get; set; }
    public string? CreatedBy { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public DateTime? DeletedTime { get; set; }

}