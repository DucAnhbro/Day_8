using Day_8.Models;
using Day_8.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Day_8.Controllers
{
    public class AccountController : Controller
    {
        EmployessContext _db = new EmployessContext();
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            {
                var p = _db.Employes.ToList();
                var userDetail = _db.Employes.SingleOrDefault(x => x.Email == email && x.Password == password);

                if (userDetail == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    HttpContext.Session.SetString(Session.ID, email);

                    return RedirectToAction("Index", "Employe");
                }

            }
        }

        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Employe _employe)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Employes.FirstOrDefault(s => s.Email == _employe.Email);
                if (check == null)
                {
                    _db.Employes.Add(_employe);
                    _db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }
    }
}
