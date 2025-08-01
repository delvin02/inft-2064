using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AOWebApp.Controllers
{
    public class TestController : Controller
    {
        // GET: TestController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RazorTest(int id, string query, string formValue)
        {
            ViewBag.Id = id;
            ViewBag.Query = query;
            ViewBag.FormValue = formValue;
            return View();
        }
    }
}
