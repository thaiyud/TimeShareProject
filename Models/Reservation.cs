using System;
using System.Collections.Generic;

namespace TimeShareProject.Models;

public partial class Reservation : BaseEntity
{

    public string PropertyId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime? RegisterDate { get; set; }
    public int? YearQuantity { get; set; }
    public int? Type { get; set; }
    public string BlockId { get; set; } = string.Empty;
    public int? Status { get; set; }
    public int? Order { get; set; }
    public virtual Block Block { get; set; } = null!;
    public virtual Property? Property { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public virtual ApplicationUser User { get; set; } = null!;
}
