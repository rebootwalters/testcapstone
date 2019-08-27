using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestCapstoneWeb
{
    public class OneViewTwoTablesModel
    {
        // User Stuff
        [Required]
        public string EMail { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Password)]
        [Compare("PasswordAgain",ErrorMessage ="Passwords do not Match")]
        [Required]
        [StringLength(MagicConstants.MaxPasswordLength,
            ErrorMessage = "The {0} must be between {2} and {1} characters long.",
            MinimumLength = MagicConstants.MinPasswordLength)]
        [RegularExpression(MagicConstants.PasswordRequirements,
            ErrorMessage = MagicConstants.PasswordRequirementsMessage)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not Match")]
        [Required]
        [StringLength(MagicConstants.MaxPasswordLength,
            ErrorMessage = "The {0} must be between {2} and {1} characters long.",
            MinimumLength = MagicConstants.MinPasswordLength)]
        [RegularExpression(MagicConstants.PasswordRequirements,
            ErrorMessage = MagicConstants.PasswordRequirementsMessage)]
        [Display(Name = "Password Again")]
        public string PasswordAgain { get; set; }
        public int RoleID { get; set; }

        // Role Stuff
        public string NewRoleName { get; set; }

        // Item Stuff

        [Required]
        public string ItemDescription { get; set; }
        [Required]
        public decimal ItemPrice { get; set; }




    }
}