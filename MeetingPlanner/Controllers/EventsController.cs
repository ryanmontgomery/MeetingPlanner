using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MeetingPlanner.Models;

namespace MeetingPlanner.Controllers
{
    public class EventsController : Controller
    {
        private MeetingPlannerContext db = new MeetingPlannerContext();

        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
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

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            Event @event = db.Events.Find(id);
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
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            Event @event = db.Events.Find(id);
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
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Get: Events/EditAgenda/5
        public ActionResult EditAgenda(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // IEnumerable<MeetingPlanner.Models.Event> temp = db.Events.Include(m => m.Meeting).ToList().Where(e => e.Meeting.ID == id);
            return View(db.Events.Include(m => m.Meeting).ToList().Where(e => e.Meeting.ID == id).OrderBy(e => e.Order));
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
            return View("EditAgenda", db.Events.Include(m => m.Meeting).ToList().Where(e => e.Meeting.ID == agendaItem.Meeting.ID).OrderBy(e => e.Order));
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

            return View("EditAgenda", db.Events.Include(m => m.Meeting).ToList().Where(e => e.Meeting.ID == agendaItem.Meeting.ID).OrderBy(e => e.Order));
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
