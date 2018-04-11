using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingPlanner.Models.ViewModels
{
    public class AgendaItemCreate
    {
        public int MeetingID { get; set; }
        public Event Event { get; set; }
    }
}