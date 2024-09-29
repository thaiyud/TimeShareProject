using System;
using System.Collections.Generic;

namespace TimeShareProject.Models;

public partial class Contact : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool Status { get; set; }
    public string Phone { get; set; } = string.Empty;
}
