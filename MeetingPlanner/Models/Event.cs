using System.ComponentModel.DataAnnotations;

namespace MeetingPlanner.Models
{
    public class Event
    {
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string Description { get; set; }

        public int Order { get; set; }

        public Meeting Meeting { get; set; }
    }
}