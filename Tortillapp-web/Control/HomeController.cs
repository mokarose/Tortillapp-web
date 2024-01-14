using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Text;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Control
{
    public class HomeController : Controller
    {
        private readonly ISession session;
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public HomeController(IHttpContextAccessor httpContextAccessor, tortillaContext context)
        {
            this.session = httpContextAccessor.HttpContext.Session;
            _context = context;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            this.session.SetString("isFiltered", "YES");
            return Content("This action method set session variable value");
            //return View();
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
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

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
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

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
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

        /***public ActionResult GetImage()
        {
            string user = HttpContext.Session.GetString("Usuario");
            var _user = _context.UserDatas.FirstOrDefault(u => u.UserName == user);
            ViewBag["ShowPic"] = Encoding.UTF8.GetString(_user.ShowPic);

            return View();
        }***/
    }
}
