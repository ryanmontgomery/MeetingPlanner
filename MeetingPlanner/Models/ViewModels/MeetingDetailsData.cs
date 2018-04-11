using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingPlanner.Models.ViewModels
{
    public class MeetingDetailsData
    {
        public Meeting Meeting { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}