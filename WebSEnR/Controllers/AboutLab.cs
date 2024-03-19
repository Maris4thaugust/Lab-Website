using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using WebSEnR.Data;
using WebSEnR.Interface;
using WebSEnR.Interface.AboutLabInterface;
using WebSEnR.Models;
using WebSEnR.Models.AboutLabModel;
using WebSEnR.Models.ProjectsModel;
using WebSEnR.ViewModel;
using WebSEnR.ViewModel.EquipmentViewModel;
using static WebSEnR.Controllers.HomeController;

namespace WebSEnR.Controllers
{
    public class AboutLab : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly SErNDBContext _db;
        private readonly IEquipmentRepository _equipContext;

        public AboutLab(SErNDBContext db, IEquipmentRepository equipContext, IPhotoService photoService)
        {
            _photoService = photoService;
            _equipContext = equipContext;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        //---------------------------- EquipmentsController-------------------------------------
        public IActionResult EquipmentIndex()
        {
            return View("/Views/AboutLab/Equipment/EquipmentIndex.cshtml");
        }
        public async Task<IActionResult> RobotArm()
        {
            IEnumerable<Equipments> Eqtm = await _equipContext.GetAll();
            return View("/Views/AboutLab/Equipment/RobotArm.cshtml", Eqtm);
        }
 
        public async Task<IActionResult> Sensor()
        {
            IEnumerable<Equipments> Eqtm = await _equipContext.GetAll();
            return View("/Views/AboutLab/Equipment/Sensor.cshtml", Eqtm);
        }
        public async Task<IActionResult> MobileRobot()
        {
            IEnumerable<Equipments> Eqtm = await _equipContext.GetAll();
            return View("/Views/AboutLab/Equipment/MobileRobot.cshtml",Eqtm);
        }
        public async Task<IActionResult> HumanoidRobot()
        {

            IEnumerable<Equipments> Eqtm = await _equipContext.GetAll();
            return View("/Views/AboutLab/Equipment/HumanoidRobot.cshtml", Eqtm);
        }
        public async Task<IActionResult> Drone()
        {
            IEnumerable<Equipments> Eqtm = await _equipContext.GetAll();
            return View("/Views/AboutLab/Equipment/Drone.cshtml",Eqtm);
        }

        public IActionResult CreateEquipment()
        {
            return View("/Views/AboutLab/Equipment/CreateEquipment.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> CreateEquipment(CreateEquipmentViewModel EqtmVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(EqtmVM.Image);
                var Eqmt = new Equipments
                {
                    Tittle = EqtmVM.Tittle,
                    Description = EqtmVM.Description,
                    Image = result.Url.ToString(),
                    EquipmentCategory = EqtmVM.EquipmentCategory,
                    ProductId = EqtmVM.ProductId
                };
                _equipContext.Add(Eqmt);
                var Category = Eqmt.EquipmentCategory.ToString();
                switch (Category)
                {
                    case "RobotArm":
                        return RedirectToAction("RobotArm");
                        break;
                    case "MobileRobot":
                        return RedirectToAction("MobileRobot");
                        break;
                    case "Drone":
                        return RedirectToAction("Drone");
                        break;
                    case "HumanoidRobot":
                        return RedirectToAction("HumanoidRobot");
                        break;
                }
            }
            else
            {
                ModelState.AddModelError("", "Create Failed");
            }
            return View("/Views/AboutLab/Equipment/CreateEquipment.cshtml", EqtmVM);
        }
        public async Task<IActionResult> EditEquipment(int id)
        {
            var Eqtm = await _equipContext.GetByIdAsync(id);
            if (Eqtm == null) return View("Error!");
            var EqtmVM = new EditEquipmentViewModel
            {
                Tittle = Eqtm.Tittle,
                Description = Eqtm.Description,
                URL = Eqtm.Image,
                EquipmentCategory = Eqtm.EquipmentCategory,
                ProductId = Eqtm.ProductId
            };
            return View("/Views/AboutLab/Equipment/EditEquipment.cshtml", EqtmVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditEquipment(int id, EditEquipmentViewModel EqtmVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit race");
                return View("/Views/AboutLab/Equipment/EditEquipment.cshtml", EqtmVM);
            }
            var userRace = await _equipContext.GetByIdAsyncNoTracking(id);
            if (userRace != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userRace.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(EqtmVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(EqtmVM.Image);
                var Eqtm = new Equipments
                {
                    Id = id,
                    Tittle = EqtmVM.Tittle,
                    Description = EqtmVM.Description,
                    Image = photoResult.Url.ToString(),
                    ProductId = EqtmVM.ProductId,
                    EquipmentCategory = EqtmVM.EquipmentCategory
                };
                _equipContext.Update(Eqtm);
                var Category = Eqtm.EquipmentCategory.ToString();
                switch (Category)
                {
                    case "RobotArm":
                        return RedirectToAction("RobotArm");
                        break;
                    case "MobileRobot":
                        return RedirectToAction("MobileRobot");
                        break;
                    case "Drone":
                        return RedirectToAction("Drone");
                        break;
                    case "HumanoidRobot":
                        return RedirectToAction("HumanoidRobot");
                        break;
                }
            }
            else
            {
                ModelState.AddModelError("", "Create Failed");
            }
            return View("/Views/AboutLab/Equipment/EditEquipment.cshtml", EqtmVM);
        }
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var equipDetails = await _equipContext.GetByIdAsync(id);
            if (equipDetails == null) return View("Error");
            return View("/Views/AboutLab/Equipment/DeleteEquipment.cshtml", equipDetails);
        }

        [HttpPost, ActionName("DeleteEquipment")]
        public async Task<IActionResult> Delete(int id)
        {
            var equipDetails = await _equipContext.GetByIdAsync(id);
            if (equipDetails == null) return View("Error");
            var Category = equipDetails.EquipmentCategory.ToString(); //Take category to return the view
            _equipContext.Delete(equipDetails);
            switch (Category)
            {
                case "RobotArm":
                    return RedirectToAction("RobotArm");
                    break;
                case "MobileRobot":
                    return RedirectToAction("MobileRobot");
                    break;
                case "Drone":
                    return RedirectToAction("Drone");
                    break;
                case "HumanoidRobot":
                    return RedirectToAction("HumanoidRobot");
                    break;
            }
            return RedirectToAction("/Views/AboutLab/Equipment/DeleteEquipment.cshtml",equipDetails);
        }


        //---------------------------- Document----------------------------------------

        public IActionResult Document()
        {
            return View("/Views/AboutLab/Document/Document.cshtml");
        }
        public IActionResult ReferenceBook()
        {
            return View("/Views/AboutLab/Document/ReferenceBook.cshtml");
        }
        public IActionResult ReferenceGraduateProject()
        {
            return View("/Views/AboutLab/Document/ReferenceGraduateProject.cshtml");
        }

        //---------------------------- ResearchController----------------------------------------
        public IActionResult Research()
        {
            return View("Research");
        } 
        /// <summary>
        /// Member region
        /// </summary>
        /// <returns></returns>
        public IActionResult Member()
        {
            IEnumerable<lab_member> lab_members = _db.lab_members;
            return View("Member",lab_members);
        }
        [HttpPost]
        public IActionResult DeleteMember(int id)
        {
            var obj = _db.lab_members.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.lab_members.Remove(obj);
            _db.SaveChanges();
            return View("Index");
        }
        [HttpPost]
        public IActionResult AddMemberFrQueueToMember(int id)
        {
            var obj = _db.registerqueue.Find(id);
            if (obj != null)
            {
                lab_member data = new lab_member()
                {
                    name = obj.fname,
                    school = obj.school,
                    facul = obj.faculty,
                    grade = obj.grade,
                    role = "Member",
                    desc = obj.desc_abtme,
                    joined_prjs = "",
                };

                _db.lab_members.Add(data);
                _db.registerqueue.Remove(obj);
                _db.SaveChanges();
            }
            return View("Index");
        }

        
        [HttpPost]
        public IActionResult DeleteMemberatQueue(int id)
        {
            var obj = _db.registerqueue.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.registerqueue.Remove(obj);
            _db.SaveChanges();
            return View("Product");
        }
 
        /// <summary>
        /// Product region
        /// </summary>
        /// <returns></returns>
        public IActionResult Product()
        {
            IEnumerable<labproduct> labproducts = _db.labproducts;
            return View("Product", labproducts);
        }

        [HttpPost]
        public IActionResult UpdateNewPost([FromBody] labproduct data)
        {
            _db.labproducts.Add(data);
            _db.SaveChanges();
            return View("Product");
        }

        [HttpPost]
        public IActionResult DeletePOST(int id)
        {
            var obj = _db.labproducts.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.labproducts.Remove(obj);
            _db.SaveChanges();
            return View("Product");
        }
    }
}
