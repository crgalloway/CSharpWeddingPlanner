using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeddingPlanner.Models.CustomValidations;

namespace WeddingPlanner.Models
{
    public class UserViewModel : BaseEntity
    {
        [Required(ErrorMessage="First name required")]
        [MinLength(2, ErrorMessage="Last name must be 2 characters or longer")]
        [Display(Name="First Name: ")]
        public string first_name{get;set;}
        [Required(ErrorMessage="Last name required")]
        [MinLength(2, ErrorMessage="Last name must be 2 characters or longer")]
        [Display(Name="Last Name: ")]
        public string last_name{get;set;}
        [EmailAddress(ErrorMessage="Must be a valid email address")]
        [Required(ErrorMessage="Email required")]
        [Display(Name="Email: ")]
        [Unique]
        public string email{get;set;}
        [Required(ErrorMessage="Password required")]
        [MinLength(8, ErrorMessage="Password must be 2 characters or longer")]
        [Display(Name="Password: ")]
        public string password{get;set;}
        [Compare("password", ErrorMessage="Password Confirmation must match")]
        [Display(Name="Confirm Password: ")]
        public string passwordconfirmation{get;set;}

    }
}