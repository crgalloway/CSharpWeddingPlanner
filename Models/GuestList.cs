using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class GuestList : BaseEntity
    {
        [Key]
        public int guestlistid{get;set;}
        [ForeignKey("User")]
        public int guestid{get;set;}
        public User User{get;set;}
        [ForeignKey("Wedding")]
        public int eventid{get;set;}
        public Wedding Wedding{get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at{get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at{get;set;}
    }
}