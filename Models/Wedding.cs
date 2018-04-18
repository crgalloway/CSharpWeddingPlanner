using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models
{
    public class Wedding : BaseEntity
    {
        [Key]
        public int weddingid {get;set;}
        public string wedder_one{get;set;}
        public string wedder_two{get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime date{get;set;}
        public string address{get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at{get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at{get;set;}
        [ForeignKey("User")]
        public int createdbyid{get;set;}
        public User CreatedBy{get;set;}
        public List<GuestList> Guests {get;set;}
        public Wedding()
        {
            Guests = new List<GuestList>();
        }
    }
}