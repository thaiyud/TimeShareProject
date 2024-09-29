using System.ComponentModel.DataAnnotations;

namespace TimeShareProject.ViewModels

{
    public class EditAccountVM
    {
        public required string CurrentUsername { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewUsername { get; set; }
        public required string NewPassword { get; set; }
    }
}