using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Public_Library.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }

        // GET: Attendance/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Attendance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attendance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Attendance/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Attendance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Attendance/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Attendance/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
