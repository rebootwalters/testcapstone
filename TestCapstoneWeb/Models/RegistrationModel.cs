using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestCapstoneWeb
{
    public class RegistrationModel
    {
        public string Message { get; set; }
        [Required]
        public string EMail { get; set; }
        [Required]
        [StringLength(MagicConstants.MaxPasswordLength,
            ErrorMessage = "The {0} must be between {2} and {1} characters long.", 
            MinimumLength = MagicConstants.MinPasswordLength)]
        [RegularExpression(MagicConstants.PasswordRequirements,
            ErrorMessage =MagicConstants.PasswordRequirementsMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password Again")]
        public string PasswordAgain { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
    }
}