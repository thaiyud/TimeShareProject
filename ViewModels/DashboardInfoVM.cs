using System;
using System.Collections.Generic;

namespace TimeShareProject.ViewModels
{
    public partial class DashboardInfoVM
    {
        public int SuccessTransaction { get; set; }
        public int TotalTransaction { get; set; }
        public int TotalMember { get; set; }
        public int TotalProject { get; set; }
        public int TotalProperty { get; set; }
        public int TotalBlock { get; set; }
        public int UndoneTransaction { get; set; }
        public int ProgressProject1 { get; set; }
        public int ProgressProject2 { get; set; }
        public int ProgressProject3 { get; set; }
        public List<string>? Months { get; set; }
        public List<int>? Amount { get; set; }
    }
}
