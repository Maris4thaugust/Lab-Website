using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebSEnR.Data;
using WebSEnR.Models;

namespace WebSEnR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SErNDBContext _db;

        public HomeController(ILogger<HomeController> logger, SErNDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Hiển thị trang đăng nhập
        public IActionResult Login()
        {
            return View("Login");
        }
        public class Account
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        [HttpPost]
        public IActionResult CheckLogin([FromBody] Account data)
        {
            if (data.username == "Admin" && data.password == "123456")
            {
                return Json(new { redirectUrl = Url.Action("CreateContent", "Home") });
            }
            else
            {
                return Json(new { });
            }
        }


        public ActionResult CreateContent()
        {
            return View("CreateContent");
        }          
        public ActionResult CensorshipRegister()
        {
            IEnumerable<RegisterQueue> registers = _db.registerqueue;
            return View("CensorshipRegister",registers);
        }
        [HttpPost]
        public IActionResult AddMemberToQueue([FromBody] RegisterQueue data)
        {
            _db.registerqueue.Add(data);
            _db.SaveChanges();
            return View("Index");
        }




        public ActionResult CensorshipPostEtp()
        {
            IEnumerable<post_etp_queue> posts_etp_queue = _db.posts_etp_queue;
            return View("CensorshipPostEtp", posts_etp_queue);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return base.View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}