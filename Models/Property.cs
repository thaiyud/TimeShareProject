using System;
using System.Collections.Generic;

namespace TimeShareProject.Models;

public partial class Property : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public double UnitPrice { get; set; }
    public string ProjectId { get; set; } = string.Empty;
    public int? Beds { get; set; }
    public string? Occupancy { get; set; }
    public string? Size { get; set; }
    public string? Bathroom { get; set; }
    public string? Views { get; set; }
    public string? UniqueFeature { get; set; }
    public string? ViewImage { get; set; }
    public string? FrontImage { get; set; }
    public string? InsideImage { get; set; }
    public string? SideImage { get; set; }
    public bool? Status { get; set; }
    public virtual Project Project { get; set; } = null!;
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
