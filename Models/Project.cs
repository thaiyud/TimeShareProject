using System;
using System.Collections.Generic;

namespace TimeShareProject.Models;

public partial class Project : BaseEntity
{
    public string ShortName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int? TotalUnit { get; set; }
    public string? Image1 { get; set; }
    public string? Image2 { get; set; }
    public string? Image3 { get; set; }
    public string? GeneralDescription { get; set; }
    public string? DetailDescription { get; set; }
    public bool? Status { get; set; }
    public int? Area { get; set; }
    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
    public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();
}
