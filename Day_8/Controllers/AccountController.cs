using Microsoft.AspNetCore.Mvc;
using Day_8.Models;
using Day_8.Constants;
using Microsoft.EntityFrameworkCore;
using Day_8.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Day_8.Controllers
{
    public class AccountController : Controller
    {
        EmployessContext _db = new EmployessContext();
        public AccountController(EmployessContext context)
        {
            _db = context;
        }
        public IActionResult Login()
        {
            return View();
        }


        //[HttpPost, ActionName("Login")]
        //public async Task<IActionResult> Login(string email, string password, int id)
        //{
        //    var CheckAccount = _db.Groups.Any(a => a.Id == id);
        //    var UserExit = _db.Employes.Any(x => x.Email == email && x.Password == password);
        //    if (!UserExit)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    else
        //    {
        //        HttpContext.Session.SetString("auth", email);

        //        return RedirectToAction("Index", "Employe");
        //    }
        //}

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            {
                var roleEmployeContext = _db.Employes.Include(u => u.Group);

                var p = roleEmployeContext.ToList();
                var userDetail = p.SingleOrDefault(x => x.Email == email && x.Password == password);


                // create claims
                List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, userDetail.Email),
        new Claim(ClaimTypes.CookiePath, userDetail.Password)
    };

                // create identity
                ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

                // create principal
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                // sign-in
                await HttpContext.SignInAsync(
                        scheme: "DemoSecurityScheme",
                        principal: principal,
                        properties: new AuthenticationProperties
                        {
                            //IsPersistent = true, // for 'remember me' feature
                            //ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                        });


                if (userDetail == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    HttpContext.Session.SetString(Session.ID, email);
                    HttpContext.Session.SetInt32("Id", userDetail.Id);


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

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync(
           scheme: "DemoSecurityScheme");
            HttpContext.Session.Remove(Session.ID);
            return RedirectToAction("Login", "Login");
        }

        public ActionResult GuestPage()
        {
            HttpContext.Session.Remove(Session.ID);
            return View();
        }
    }
}
