using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingPlanner.Models
{
    public class Meeting
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Meeting Date")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MeetingDate { get; set; }

        [Display(Name = "Conducting")]
        public int BishopricID { get; set; }
        public Bishopric Bishopric { get; set; }
    }
}