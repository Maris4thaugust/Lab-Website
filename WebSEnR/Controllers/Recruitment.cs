using Microsoft.AspNetCore.Mvc;
using System;
using WebSEnR.Data;
using WebSEnR.Models;

namespace WebSEnR.Controllers
{
    public class Recruitment : Controller
    {
        private readonly SErNDBContext _db;

        public Recruitment(SErNDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<post_etp> postsetp = _db.posts_etp;
            return View(postsetp);
        }
        public IActionResult EtpResgisterPosts()
        {          
            return View();
        }
        public IActionResult AbtEtps()
        {
            IEnumerable<post_etp> postsetp = _db.posts_etp;
            return View(postsetp);
        }
        public IActionResult InternInfo()
        {
            IEnumerable<post_etp> postsetp = _db.posts_etp;
            return View(postsetp);
        }
        public IActionResult FresherInfo()
        {
            IEnumerable<post_etp> postsetp = _db.posts_etp;
            return View(postsetp);
        }
        public IActionResult ResearchInfo()
        {
            IEnumerable<post_etp> postsetp = _db.posts_etp;
            return View(postsetp);
        }
        public IActionResult TrainingInfo()
        {
            IEnumerable<post_etp> postsetp = _db.posts_etp;
            return View(postsetp);
        }
        [HttpPost]
        public IActionResult UpdateNewEtpPostQueue([FromBody] post_etp_queue data)
        {
            _db.posts_etp_queue.Add(data);
            _db.SaveChanges();
            return View("Index");
        }
        [HttpPost]
        public IActionResult UpdateNewEtpPost(int id)
        {
            var obj = _db.posts_etp_queue.Find(id);
            if (obj != null)
            {
                post_etp data = new post_etp()
                {
                    title = obj.title,
                    etp_name = obj.etp_name,
                    type_i = obj.type_i,
                    type_f = obj.type_f,
                    type_t = obj.type_t,
                    type_r = obj.type_r,
                    desc_job = obj.desc_job,
                    desc_etp = obj.desc_etp,
                    contact_sdt = obj.contact_sdt,
                    contact_email = obj.contact_email,
                    exp_date = obj.exp_date,
                    post_date = obj.post_date,
                };

                _db.posts_etp.Add(data);
                _db.posts_etp_queue.Remove(obj);
                _db.SaveChanges();
            }
            return View("Index");
        }
        [HttpPost]
        public IActionResult DeleteEtpPostQueue(int id)
        {
            var obj = _db.posts_etp_queue.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.posts_etp_queue.Remove(obj);
            _db.SaveChanges();
            return View("Index");
        }
        [HttpPost]
        public IActionResult DeleteEtpPost(int id)
        {
            var obj = _db.posts_etp.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.posts_etp.Remove(obj);
            _db.SaveChanges();
            return View("Index");
        }
    }
}
