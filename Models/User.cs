using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int userid {get;set;}
        public string first_name{get;set;}
        public string last_name{get;set;}
        public string email{get;set;}
        public string password{get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at{get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at{get;set;}
        public List<GuestList> Attending {get;set;}
        public User()
        {
            Attending = new List<GuestList>();
        }
    }
}