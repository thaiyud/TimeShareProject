using Microsoft.AspNetCore.Identity;
using TimeShareProject.Models;

namespace TimeShareProject.Models;

public partial class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    public bool Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool? Status { get; set; }
    public string? Avatar { get; set; }
    public string? Address { get; set; }
    public string? IdfrontImage { get; set; }
    public string? IdbackImage { get; set; }
    public string? CreatedBy { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public DateTime? DeletedTime { get; set; }
    public virtual ICollection<New> News { get; set; } = new List<New>();
    public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
