using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MeetingPlanner.Models;

namespace MeetingPlanner.Controllers
{
    public class BishopricsController : Controller
    {
        private MeetingPlannerContext db = new MeetingPlannerContext();

        // GET: Bishoprics
        public ActionResult Index()
        {
            return View(db.Bishoprics.ToList());
        }

        // GET: Bishoprics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bishopric bishopric = db.Bishoprics.Find(id);
            if (bishopric == null)
            {
                return HttpNotFound();
            }
            return View(bishopric);
        }

        // GET: Bishoprics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bishoprics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Calling")] Bishopric bishopric)
        {
            if (ModelState.IsValid)
            {
                db.Bishoprics.Add(bishopric);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bishopric);
        }

        // GET: Bishoprics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bishopric bishopric = db.Bishoprics.Find(id);
            if (bishopric == null)
            {
                return HttpNotFound();
            }
            return View(bishopric);
        }

        // POST: Bishoprics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Calling")] Bishopric bishopric)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bishopric).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bishopric);
        }

        // GET: Bishoprics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bishopric bishopric = db.Bishoprics.Find(id);
            if (bishopric == null)
            {
                return HttpNotFound();
            }
            return View(bishopric);
        }

        // POST: Bishoprics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bishopric bishopric = db.Bishoprics.Find(id);
            db.Bishoprics.Remove(bishopric);
            db.SaveChanges();
            return RedirectToAction("Index");
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
