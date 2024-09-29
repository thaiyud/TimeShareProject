using System;
using System.Collections.Generic;

namespace TimeShareProject.Models;

public partial class Transaction : BaseEntity
{
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public bool? Status { get; set; }
    public string? TransactionCode { get; set; }
    public string ReservationId { get; set; } = string.Empty;
    public int Type { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public virtual ICollection<New> News { get; set; } = new List<New>();
    public virtual Reservation? Reservation { get; set; }
}
