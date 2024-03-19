using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSEnR.Data;
using WebSEnR.Interface;
using WebSEnR.Interface.ProjectInterface;
using WebSEnR.Models;
using WebSEnR.Models.ProjectsModel;
using WebSEnR.Repository.ProjectRepository;
using WebSEnR.ViewModel;

namespace WebSEnR.Controllers
{
    public class Projects : Controller
    {        
        private readonly SErNDBContext _db;
        private readonly IUniProjectRepository _uniContext;
        private readonly IMinisProjectRepository _minisContext;

        public Projects(SErNDBContext db, IUniProjectRepository uniContext, IMinisProjectRepository minisContext)
        {
            _db = db;
            _uniContext = uniContext;
            _minisContext = minisContext;

        }
        public IActionResult Index()
        {
            return View();
        }

        ///-----------------------------------------UniProject----------------------------------------
  
        public async Task<IActionResult> UniProjects()   
        {
            IEnumerable<UniProject> UniPjt = await _uniContext.GetAll();   
            return View("Views/Projects/UniProjects/UniProjects.cshtml",UniPjt);         
        }
        public IActionResult CreateUniProject()
        {
            return View("Views/Projects/UniProjects/CreateUniProject.cshtml");
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUniProject(CreateUniProjectViewModel UniPjtVM)
        {
            if(ModelState.IsValid)
            {
                var UniPjt = new UniProject
                {
                    Description = UniPjtVM.Description,
                    Name = UniPjtVM.Name,
                    Member = UniPjtVM.Member,
                    Href = UniPjtVM.Href
                };
                _uniContext.Add(UniPjt);
                return RedirectToAction("UniProjects");
            }
            else
            {
                ModelState.AddModelError("", "Create Failed");
            }
            return View("Views/Projects/UniProjects/CreateUniProject.cshtml", UniPjtVM);
        }
        public async Task<IActionResult> EditUniProject(int id)
        {
            var UniPjt = await _uniContext.GetByIdAsync(id);
            if (UniPjt == null) return View("Error!");
            var UniPjtVM = new EditUniProjectViewModel
            {
                Name = UniPjt.Name,
                Description = UniPjt.Description,
                Member = UniPjt.Member,
                Href = UniPjt.Href
            };
            return View("Views/Projects/UniProjects/EditUniProject.cshtml", UniPjtVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditUniProject(int id, EditUniProjectViewModel UniPjtVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View("Views/Projects/UniProjects/EditUniProject.cshtml", UniPjtVM);
            }
            var UserUniProject = await _uniContext.GetByIdAsyncNoTracking(id);
            if (UserUniProject != null)
            {
                var UniPjt = new UniProject
                {
                    Id = id,
                    Name = UniPjtVM.Name,
                    Description = UniPjtVM.Description,
                    Member = UniPjtVM.Member,
                    Href = UniPjtVM.Href
                };
                _uniContext.Update(UniPjt);
                return RedirectToAction("UniProjects");
            }
            else
            {
                return View(UniPjtVM);
            }
        } 
        public async Task<IActionResult> DeleteUniProject(int id)
        {
            var PjtDetail = await _uniContext.GetByIdAsync(id);
            if (PjtDetail == null) return View("Error!");
            return View("Views/Projects/UniProjects/DeleteUniProject.cshtml", PjtDetail);
        }

        [HttpPost, ActionName("DeleteUniProject")] 
        public async Task<IActionResult> DeleteUni(int id)
        {
            var UniPjtDetail = await _uniContext.GetByIdAsync(id);
            if (UniPjtDetail == null) return View("Error!");

            _uniContext.Delete(UniPjtDetail);
            return RedirectToAction("UniProjects");
        }
        ///-----------------------------------------MinisProject---------------------------------------

        public async Task<IActionResult> MinisProjects()
        {
            IEnumerable<MinisProject> MinisPjt = await _minisContext.GetAll();   
            return View("Views/Projects/MinisProjects/MinisProjects.cshtml", MinisPjt);         
        }
        public IActionResult CreateMinisProject()
        {
            return View("Views/Projects/MinisProjects/CreateMinisProject.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreateMinisProject(CreateMinisProjectViewModel MinisPjtVm)
        {
            if(ModelState.IsValid)
            {
                var MinisPjt = new MinisProject
                {
                    Description = MinisPjtVm.Description,
                    Name = MinisPjtVm.Name,
                    Member = MinisPjtVm.Member,
                    Href = MinisPjtVm.Href
                };
                _minisContext.Add(MinisPjt);
                return RedirectToAction("MinisProjects");
            }
            else
            {
                ModelState.AddModelError("", "Create Failed");
            }
            return View("Views/Projects/MinisProjects/CreateMinisProject.cshtml",MinisPjtVm);
        }

        public async Task<IActionResult> EditMinisProject(int id)
        {
            var MinisPjt = await _minisContext.GetByIdAsync(id);
            if (MinisPjt == null) return View("Error!");
            var MinisPjtVM= new EditMinisProjectViewModel
            {
                Name = MinisPjt.Name,
                Description = MinisPjt.Description,
                Member = MinisPjt.Member,
                Href = MinisPjt.Href
            };
            return View("Views/Projects/MinisProjects/EditMinisProject.cshtml", MinisPjtVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditMinisProject(int id, EditMinisProjectViewModel MinisPjtVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View("Views/Projects/MinisProjects/EditMinisProject.cshtml", MinisPjtVM);
            } 
            var UserMinisProject = await _minisContext.GetByIdAsyncNoTracking(id);
            if (UserMinisProject != null)
            {
                var MinisPjt = new MinisProject
                {
                    Id = id,
                    Name = MinisPjtVM.Name,
                    Description = MinisPjtVM.Description,
                    Member = MinisPjtVM.Member,
                    Href = MinisPjtVM.Href
                };
                _minisContext.Update(MinisPjt);
                return RedirectToAction("MinisProjects");
            }
            else
            {
                return View("Views/Projects/MinisProjects/EditMinisProject.cshtml", MinisPjtVM);
            }
        }
        public async Task<IActionResult> DeleteMinisProject(int id)
        {
            var PjtDetail = await _minisContext.GetByIdAsync(id);
            if (PjtDetail == null) return View("Error!");
            return View("Views/Projects/MinisProjects/DeleteMinisProject.cshtml",PjtDetail);
        }

        [HttpPost, ActionName("DeleteMinisProject")]
        public async Task<IActionResult> DeleteMinis(int id)
        {
            var MinisPjtDetail = await _minisContext.GetByIdAsync(id);
            if (MinisPjtDetail == null) return View("Error!");

            _minisContext.Delete(MinisPjtDetail);
            return RedirectToAction("MinisProjects");
        }
        
        ///-----------------------------------------EtpProject-----------------------------

        public IActionResult EtpProjects() 
        {
            
            return View("EtpProjects");
        }
        [HttpPost]
        public IActionResult AddEtpProjects([FromBody] enterprise_project data)
        {
            _db.enterpriseprojects.Add(data);
            _db.SaveChanges();
            return View("EtpProjects");
        }
        [HttpPost]
        public IActionResult DeleteEtpProjects(int id)
        {
            var obj = _db.enterpriseprojects.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.enterpriseprojects.Remove(obj);
            _db.SaveChanges();
            return View("EtpProjects");
        }
        public IActionResult SSD()
        {
            IEnumerable<enterprise_project> etps = _db.enterpriseprojects;
            return View("SSD", etps);
        }
        public IActionResult SSE()
        {
            IEnumerable<enterprise_project> etps = _db.enterpriseprojects;
            return View("SSE", etps);
        }
        public IActionResult NV()
        {
            IEnumerable<enterprise_project> etps = _db.enterpriseprojects;
            return View("NV", etps);
        }
        public IActionResult ATM()
        {
            IEnumerable<enterprise_project> etps = _db.enterpriseprojects;
            return View("ATM", etps);
        }
        public IActionResult VDK()
        {
            IEnumerable<enterprise_project> etps = _db.enterpriseprojects;
            return View("VDK", etps);
        }
    }
}
