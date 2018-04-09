using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingPlanner.Models
{
    public class Meeting
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Meeting")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MeetingDate { get; set; }

        public int BishopricID { get; set; }
        [Display(Name = "Conducting")]
        public Bishopric Bishopric { get; set; }
    }
}