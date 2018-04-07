using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeetingPlanner.Models
{
    public class MeetingPlannerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MeetingPlannerContext() : base("name=MeetingPlannerContext")
        {
        }

        public System.Data.Entity.DbSet<MeetingPlanner.Models.Bishopric> Bishoprics { get; set; }

        public System.Data.Entity.DbSet<MeetingPlanner.Models.Meeting> Meetings { get; set; }

        public System.Data.Entity.DbSet<MeetingPlanner.Models.Event> Events { get; set; }
    }
}
