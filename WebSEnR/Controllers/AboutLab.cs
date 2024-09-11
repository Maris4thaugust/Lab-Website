using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Reflection;
using WebSEnR.Data;
using WebSEnR.Interface;
using WebSEnR.Interface.AboutLabInterface;
using WebSEnR.Models;
using WebSEnR.Models.AboutLabModel;
using WebSEnR.Models.ProjectsModel;
using WebSEnR.ViewModel;
using WebSEnR.ViewModel.DocumentViewModel;
using WebSEnR.ViewModel.EquipmentViewModel;
using WebSEnR.ViewModel.ProjectViewModel;
using static WebSEnR.Controllers.HomeController;

namespace WebSEnR.Controllers
{
    public class AboutLab : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly SErNDBContext _db;
        private readonly IEquipmentRepository _equipContext;
        private readonly IDocumentRepository _docContext;

        public AboutLab(SErNDBContext db, IEquipmentRepository equipContext, IPhotoService photoService, IDocumentRepository docContext)
        {
            _photoService = photoService;
            _equipContext = equipContext;
            _db = db;
            _docContext = docContext;

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
                    Title = EqtmVM.Title,
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
                    case "Sensor":
                        return RedirectToAction("Sensor");
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
                Title = Eqtm.Title,
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
                    Title = EqtmVM.Title,
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
                    case "Sensor":
                        return RedirectToAction("Sensor");
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
                case "Sensor":
                    return RedirectToAction("Sensor");
                    break;
            }
            return RedirectToAction("/Views/AboutLab/Equipment/DeleteEquipment.cshtml",equipDetails);
        }


        //---------------------------- Document----------------------------------------

        public IActionResult Document()
        {
            return View("/Views/AboutLab/Document/Document.cshtml");
        }
        public async Task<IActionResult> ReferenceBook()
        {
            IEnumerable<DocumentModel> Doc = await _docContext.GetAll();
            return View("/Views/AboutLab/Document/ReferenceBook.cshtml",Doc);
        }
        public async Task<IActionResult> ReferenceGraduateProject()
        {
            IEnumerable<DocumentModel> Doc = await _docContext.GetAll();
            return View("/Views/AboutLab/Document/ReferenceGraduateProject.cshtml", Doc);
        }
        public IActionResult CreateDocument()
        {
            return View("/Views/AboutLab/Document/CreateDocument.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> CreateDocument(CreateDocumentViewModel DocVM)
        {
            if (ModelState.IsValid)
            {
                var Doc = new DocumentModel
                {
                    Title = DocVM.Title,
                    Url = DocVM.Url,
                    DocumentCategory = DocVM.DocumentCategory
                };
                _docContext.Add(Doc);
                var Category = Doc.DocumentCategory.ToString();
                switch (Category)
                {
                    case "ReferenceBook":
                        return RedirectToAction("ReferenceBook");
                        break;
                    case "ReferenceGraduateProject":
                        return RedirectToAction("ReferenceGraduateProject");
                        break;

                }
            }
            else
            {
                ModelState.AddModelError("", "Create Failed");
            }
            return View("/Views/AboutLab/Document/CreateDocument.cshtml", DocVM);
        }
        public async Task<IActionResult> EditDocument(int id)
        {
            var Doc = await _docContext.GetByIdAsync(id);
            if (Doc == null) return View("Error!");
            var DocVM = new EditDocumentViewModel
            {
                Title = Doc.Title,
                Url = Doc.Url,
                DocumentCategory = Doc.DocumentCategory

            };
            return View("/Views/AboutLab/Document/EditDocument.cshtml", DocVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditDocument(int id, EditDocumentViewModel DocVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View("Views/Projects/Document/EditDocument.cshtml", DocVM);
            }
            var UserDocument = await _docContext.GetByIdAsyncNoTracking(id);
            if (UserDocument != null)
            {
                var Doc = new DocumentModel
                {
                    Id = id,
                    Title = DocVM.Title,
                    Url = DocVM.Url,
                    DocumentCategory = DocVM.DocumentCategory

                };
                _docContext.Update(Doc);
                var Category = Doc.DocumentCategory.ToString();
                switch (Category)
                {
                    case "ReferenceBook":
                        return RedirectToAction("ReferenceBook");
                        break;
                    case "ReferenceGraduateProject":
                        return RedirectToAction("ReferenceGraduateProject");
                        break;
                }
            }
            else
            {
                ModelState.AddModelError("", "Create Failed");
            }
            return View("/Views/AboutLab/Document/EditDocument.cshtml", DocVM);
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
            IEnumerable<LabMembers> lab_members = _db.lab_members;
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
                LabMembers data = new LabMembers()
                {
                    Name = obj.Name,
                    School = obj.School,
                    Major = obj.Major,
                    Grade = obj.Grade,
                    MSSV = "Member",
                    Project = obj.Project,
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
