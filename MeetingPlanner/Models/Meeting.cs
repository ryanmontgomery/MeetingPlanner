using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingPlanner.Models
{
    public class Meeting
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Meeting Date")]
        public DateTime MeetingDate { get; set; }

        public int BishopricID { get; set; }
        public Bishopric Bishopric { get; set; }
    }
}