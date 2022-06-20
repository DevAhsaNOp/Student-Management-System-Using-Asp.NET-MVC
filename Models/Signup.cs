using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Student_Management_System.Models
{
    [Table("tbl_account")]
    public class Signup
    {
        [Key]
        public int userid { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Name Required!")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [StringLength(50)]
        [EmailAddress]
        [Required(ErrorMessage = "Email Required!")]
        [Display(Name = "Email Address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Does not Match!")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string Confirmpassword { get; set; }

    }
}