using DAO;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        UserDAO uu = new UserDAO();
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult View1()
        {
           
            return View();
        }
        public ActionResult Top()
        {
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        public ActionResult Left()
        {
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        public ActionResult Main()
        {
            
            ViewBag.rs = uu.Rs();
            ViewBag.rc = uu.Rs1();
            ViewBag.jg = uu.Rs2();
            ViewBag.xc = uu.Rs3();
            ViewBag.s = uu.Rs5();
            return View();
        }
        public async Task<ActionResult> Xx()
        {
            return Json(await uu.Rs4(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<int> Login(User u)
        {

            User re = await uu.LoginAsync(u);
            if (re != null)
            {
                Session["user"] = re;
              
                return 1;
            }
            else
            {
                return 0;

            }
        }
    


        

        public async Task<ActionResult> Ll( int rid)
        {

            List<Trees> jj = await uu.GetList(rid);
            return Json(jj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}