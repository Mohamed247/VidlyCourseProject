using System.ComponentModel.DataAnnotations;

namespace VidlyProject.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Driving License")]
        public string DrivingLicense { get; set; }
    }
}
