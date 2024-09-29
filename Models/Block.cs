﻿using System;
using System.Collections.Generic;

namespace TimeShareProject.Models;

public partial class Block : BaseEntity
{
    public int StartDay { get; set; }
    public int StartMonth { get; set; }
    public int EndDay { get; set; }
    public int EndMonth { get; set; }
    public int BlockNumber { get; set; }
    public double? Proportion { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
