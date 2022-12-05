using Day_8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day_8.Controllers
{
    public class EmployeController : Controller
    {
        private EmployessContext _db = new EmployessContext();

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult List()
        //{
        //    return View(_db.Employes.ToList());
        //}

        //public ActionResult ShowDetail()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Add(Employe employe)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var check = _db.Employes.FirstOrDefault(s => s.Id == employe.Id);
        //        if (check == null)
        //        {
        //            _db.Employes.Add(employe);
        //            _db.SaveChanges();
        //            return RedirectToAction("List");
        //        }
        //        else
        //        {
        //            ViewBag.error = "ID exits";
        //            return View();
        //        }


        //    }
        //    return View();
        //}
        [HttpGet]
        public async Task<IActionResult> ShowDetail()
        {
            var userEmail = HttpContext.Session.GetString("auth");
            if ( string.IsNullOrEmpty(userEmail) || _db.Employes == null)
            {
                return NotFound();
            }

            var em = await _db.Employes
                .FirstOrDefaultAsync(m => m.Email == userEmail);
            if (em == null)
            {
                return NotFound();
            }


            return View(em);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Employes == null)
            {
                return NotFound();
            }

            var em = await _db.Employes.FindAsync(id)
;
            if (em == null)
            {
                return NotFound();
            }
            return View(em);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employe employe)
        {
            if (id != employe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(employe);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployesExists(employe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employe);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Employes == null)
            {
                return NotFound();
            }

            var em = await _db.Employes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (em == null)
            {
                return NotFound();
            }

            return View(em);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Employes == null)
            {
                return Problem("Entity set 'DataUserContext.Usertbls'  is null.");
            }
            var user = await _db.Employes.FindAsync(id)
;
            if (user != null)
            {
                _db.Employes.Remove(user);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool EmployesExists(int id)
        {
            return _db.Employes.Any(e => e.Id == id);
        }

        

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("Id");
            return RedirectToAction("Login", "Account");
        }
    }
}
