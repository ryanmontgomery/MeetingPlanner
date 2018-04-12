using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MeetingPlanner.Models;
using MeetingPlanner.Models.ViewModels;

namespace MeetingPlanner.Controllers
{
    public class EventsController : Controller
    {
        private MeetingPlannerContext db = new MeetingPlannerContext();

        // GET: Events
        public ActionResult Index()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create/5
        public ActionResult Create(int? id)
        {
            AgendaItemCreate item = new AgendaItemCreate();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            item.MeetingID = (int)id;
            return View(item);
        }

        // POST: Events/Create/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,MeetingID,Order")] Event @event, AgendaItemCreate item)
        {
            if (db.Meetings.Find(item.MeetingID) == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var orderCount = db.Events.Include(m => m.Meeting).ToList().Where(e => e.Meeting.ID == item.MeetingID).Count();

                //@event.Meeting.ID = item.MeetingID; //This is bad why oh why would you do this?
                @event.Meeting = db.Meetings.Find(item.MeetingID);
                @event.Order = orderCount + 1;
                                
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("EditAgenda", new { id = item.MeetingID });
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Include(m => m.Meeting).Single(e => e.ID == id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Order")] Event @event)
        {
            if (ModelState.IsValid)
            {
                //var agendaItem = db.Events.Include(m => m.Meeting).Single(e => e.ID == @event.ID);
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditAgenda", new { db.Events.Include(m => m.Meeting).Single(e => e.ID == @event.ID).Meeting.ID });
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Include(m => m.Meeting).Single(e => e.ID == id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            var meetingId = db.Events.Include(m => m.Meeting).Single(e => e.ID == @event.ID).Meeting.ID;
            var eventReorder = db.Events.Include(m => m.Meeting).ToList().Where(e => e.Meeting.ID == meetingId);

            if (eventReorder.Count() > @event.Order)
            {
                foreach(var agendaItem in eventReorder)
                {
                    if(agendaItem.Order > @event.Order)
                    {
                        agendaItem.Order -= 1;
                    }
                    
                }
            }

            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("EditAgenda", new { id = meetingId });
        }

        // Get: Events/EditAgenda/5
        public ActionResult EditAgenda(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = new MeetingDetailsData();
            viewModel.Meeting = db.Meetings.Include(m => m.Bishopric).Where(bob => bob.ID == id).FirstOrDefault();
            if (viewModel.Meeting == null)
            {
                return HttpNotFound();
            }
            viewModel.Events = db.Events.Include(m => m.Meeting).ToList().Where(e => e.Meeting.ID == id).OrderBy(e => e.Order);
            return View(viewModel);
        }

        // Post: Events/OrderUp/5
        public ActionResult OrderUp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var agendaItem = db.Events.Include(m => m.Meeting).Single(e => e.ID == id);
            
            if (agendaItem.Order > 1)
            {
                var agendaItem2 = db.Events.Include(m => m.Meeting).Single(e => e.Order == agendaItem.Order - 1 && e.Meeting.ID == agendaItem.Meeting.ID);
                agendaItem.Order = agendaItem.Order - 1;
                agendaItem2.Order = agendaItem2.Order + 1;
                db.SaveChanges();
            }
            return RedirectToAction("EditAgenda", new { agendaItem.Meeting.ID });
            //return View("EditAgenda", db.Events.Include(m => m.Meeting).ToList().Where(e => e.Meeting.ID == agendaItem.Meeting.ID).OrderBy(e => e.Order));
        }

        // Post: Events/OrderDown/5
        public ActionResult OrderDown(int? id)
        {
            var agendaItem = db.Events.Include(m => m.Meeting).Single(e => e.ID == id);
            var agendaSize = db.Events.Include(m => m.Meeting).Where(e => e.Meeting.ID == agendaItem.Meeting.ID).Count();

            if(agendaItem.Order < agendaSize)
            {
                var agendaItem2 = db.Events.Include(m => m.Meeting).Single(e => e.Order == agendaItem.Order + 1 && e.Meeting.ID == agendaItem.Meeting.ID);
                agendaItem.Order = agendaItem.Order + 1;
                agendaItem2.Order = agendaItem2.Order - 1;
                db.SaveChanges();
            }
            return RedirectToAction("EditAgenda", new { agendaItem.Meeting.ID });
            //return View("EditAgenda", db.Events.Include(m => m.Meeting).ToList().Where(e => e.Meeting.ID == agendaItem.Meeting.ID).OrderBy(e => e.Order));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
