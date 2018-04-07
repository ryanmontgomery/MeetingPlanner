using System.ComponentModel.DataAnnotations;

namespace MeetingPlanner.Models
{
    public class Bishopric
    {
        public int ID { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }


        [StringLength(50, MinimumLength = 1)]
        public string Calling { get; set; }
    }
}