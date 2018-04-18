using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeddingPlanner.Models.CustomValidations;

namespace WeddingPlanner.Models
{
    public class WeddingViewModel : BaseEntity
    {
        [Required(ErrorMessage="Name of bride/groom required")]
        [MinLength(2, ErrorMessage="Name must be longer than 2 characters")]
        [Display(Name="Wedder One: ")]
        public string wedder_one{get;set;}
        [Required(ErrorMessage="Name of bride/groom required")]
        [MinLength(2, ErrorMessage="Name must be longer than 2 characters")]
        [Display(Name="Wedder Two: ")]

        public string wedder_two{get;set;}
        [DataType(DataType.Date)]
        [Required(ErrorMessage="Date required")]
        [InTheFuture]
        [Display(Name="Date of Event: ")]
        public DateTime? date{get;set;}
        [Display(Name="Address: ")]
        [Required(ErrorMessage="Address required")]
        public string address{get;set;}
    }
}