using Microsoft.AspNetCore.Identity;


namespace TimeShareProject.Models;
public class ApplicationUserLogins : IdentityUserLogin<Guid>
{
    public string? CreatedBy { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public DateTime? DeletedTime { get; set; }
}
