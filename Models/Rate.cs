using System;
using System.Collections.Generic;

namespace TimeShareProject.Models;

public partial class Rate
{
    public string ProjectId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string? DetailRate { get; set; }
    public int? StarRate { get; set; }
    public virtual Project Project { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;
}
