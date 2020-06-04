using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace adminapi.Controllers
{
    public class HomeController : Controller
    {
        DatabaseEntities1 databas = new DatabaseEntities1();

        public ActionResult Index()
        {
            if (Session["is_logged_in"] == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.arrangörer = databas.Arrangörer.ToList();
            ViewBag.besökare = databas.Besökare.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Index(string first, string last, string email, string password, string roll)
        {
            switch (roll)
            {
                case "Arrangör":
                    var nyArrangör = new Arrangörer
                    {
                        Firstname = first,
                        Lastname = last,
                        Email = email,
                        Password = password,
                        Role = roll
                    };
                    databas.Arrangörer.Add(nyArrangör);
                    databas.SaveChanges();
                    break;
                case "Besökare":
                    var nybesökare = new Besökare
                    {
                        Firstname = first,
                        Lastname = last,
                        Email = email,
                        Password = password,
                        Role = roll
                    };
                    databas.Besökare.Add(nybesökare);
                    databas.SaveChanges();
                    break;
            }
            return RedirectToAction("index");
        }

        public ActionResult Arrangör(int id)
        {
            var arrangör = databas.Arrangörer.Find(id);
            return View(arrangör);
        }

        [HttpPost]
        public ActionResult Arrangör(Arrangörer uppdaterad)
        {
            databas.Entry(uppdaterad).State = System.Data.Entity.EntityState.Modified;
            databas.SaveChanges();
            return RedirectToAction("index");
        }


        public ActionResult Besökare(int id)
        {
            var besökare = databas.Besökare.Find(id);
            return View(besökare);
        }

        [HttpPost]
        public ActionResult Besökare(Besökare uppdaterad)
        {
            databas.Entry(uppdaterad).State = System.Data.Entity.EntityState.Modified;
            databas.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Rma(int id)
        {
            var findUser = databas.Arrangörer.Find(id);
            databas.Arrangörer.Remove(findUser);
            databas.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Rmb(int id)
        {
            var findUser = databas.Besökare.Find(id);
            databas.Besökare.Remove(findUser);
            databas.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult InsertAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertAdmin(string user, string pass, string perm)
        {
            Admins_Login nyAdmin = new Admins_Login
            {
                username = user,
                password = pass,
                permission = perm
            };
            databas.Admins_Login.Add(nyAdmin);
            databas.SaveChanges();
            return RedirectToAction("InsertAdmin");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {
                var findUser = databas.Admins.SingleOrDefault(user => user.username == username);
                if (findUser.password == password)
                {
                    Session["is_logged_in"] = true;
                    return RedirectToAction("index");
                }
            } catch (NullReferenceException)
            {
                return Content("Incorrect details. Please try again");
            }
            return null;

        }
        
        public ActionResult Logout()
        {
            Session["is_logged_id"] = null;
            return RedirectToAction("Login");
        }
    }
}