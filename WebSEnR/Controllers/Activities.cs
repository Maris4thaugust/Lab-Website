using Microsoft.AspNetCore.Mvc;
using WebSEnR.Interface;
using WebSEnR.Interface.ActivityInterface;
using WebSEnR.Models;
using WebSEnR.Models.AboutLabModel;
using WebSEnR.ViewModel.ActivityViewModel;
using WebSEnR.ViewModel.EquipmentViewModel;



namespace WebSEnR.Controllers
{
    public class Activities : Controller
    {
        private readonly IActivityRepository _activityContext;
        private readonly IPhotoService _photoService;

        public Activities(IActivityRepository activityContext, IPhotoService photoService)
        {
            _activityContext = activityContext;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Activity> Act = await _activityContext.GetAll();
            return View("/Views/Activities/Index.cshtml", Act);
        }

        public async Task<IActionResult> AfterUniActivity()
        {
            IEnumerable<Activity> Act = await _activityContext.GetAll();
            return View("/Views/Activities/AfterUniActivity.cshtml", Act);
        }
        public async Task<IActionResult> Seminar()
        {
            IEnumerable<Activity> Act = await _activityContext.GetAll();
            return View("/Views/Activities/Seminar.cshtml", Act);
        }
        public async Task<IActionResult> EnterpriseCooperate()
        {
            IEnumerable<Activity> Act = await _activityContext.GetAll();
            return View("/Views/Activities/EnterpriseCooperate.cshtml", Act);
        }
        public async Task<IActionResult> Others()
        {
            IEnumerable<Activity> Act = await _activityContext.GetAll();
            return View("/Views/Activities/Others.cshtml", Act);
        }
        public IActionResult CreateActivity()
        {
            return View("/Views/Activities/CreateActivity.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> CreateActivity(CreateActivityViewModel ActVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(ActVM.Image);
                var Act = new Activity
                {
                    Title = ActVM.Title,
                    Description = ActVM.Description,
                    Image = result.Url.ToString(),
                    ActivityCategory = ActVM.ActivityCategory,
                    Url = ActVM.Url
                };
                _activityContext.Add(Act);
                var Category = Act.ActivityCategory.ToString();
                switch (Category)
                {
                    case "AfterUniActivity":
                        return RedirectToAction("AfterUniActivity");
                        break;
                    case "Seminar":
                        return RedirectToAction("Seminar");
                        break;
                    case "EnterpriseCooperate":
                        return RedirectToAction("EnterpriseCooperate");
                        break;
                    case "Others":
                        return RedirectToAction("Others");
                        break;
                }
            }
            else
            {
                ModelState.AddModelError("", "Create Failed");
            }
            return View("/Views/Activities/CreateActivity.cshtml", ActVM);
        }
        public async Task<IActionResult> EditActivity(int id)
        {
            var Act = await _activityContext.GetByIdAsync(id);
            if (Act == null) return View("Error!");
            var ActVM = new EditActivityViewModel
            {
               Title = Act.Title,
               Description = Act.Description,
               ActivityCategory = Act.ActivityCategory,
               Url = Act.Url,
               ImageUrl = Act.Image
            };
            return View("/Views/Activities/EditActivity.cshtml", ActVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditActivity(int id, EditActivityViewModel ActVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit activity");
                return View("/Views/Activities/EditActivity.cshtml", ActVM);
            }
            var userRace = await _activityContext.GetByIdAsyncNoTracking(id);
            if (userRace != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userRace.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View("/Views/Activities/EditActivity.cshtml", ActVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(ActVM.Image);
                var Act = new Activity
                {
                    Id = id,
                    Title = ActVM.Title,
                    Description = ActVM.Description,
                    Image = photoResult.Url.ToString(),
                    Url = ActVM.Url,
                    ActivityCategory = ActVM.ActivityCategory
                };
                _activityContext.Update(Act);
                var Category = Act.ActivityCategory.ToString();
                switch (Category)
                {
                    case "AfterUniActivity":
                        return RedirectToAction("AfterUniActivity");
                        break;
                    case "Seminar":
                        return RedirectToAction("Seminar");
                        break;
                    case "EnterpriseCooperate":
                        return RedirectToAction("EnterpriseCooperate");
                        break;
                    case "Others":
                        return RedirectToAction("Others");
                        break;
                }
            }
            else
            {
                ModelState.AddModelError("", "Edit Failed");
            }
            return View("/Views/Activities/EditActivity.cshtml", ActVM);
        }
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activityDetails = await _activityContext.GetByIdAsync(id);
            if (activityDetails == null) return View("Error");
            return View("/Views/Activities/DeleteActivity.cshtml", activityDetails);
        }

        [HttpPost, ActionName("DeleteActivity")]
        public async Task<IActionResult> Delete(int id)
        {
            var activityDetails = await _activityContext.GetByIdAsync(id);
            if (activityDetails == null) return View("Error");
            var Category = activityDetails.ActivityCategory.ToString(); //Take category to return the view
            _activityContext.Delete(activityDetails);
            switch (Category)
            {
                case "AfterUniActivity":
                    return RedirectToAction("AfterUniActivity");
                    break;
                case "Seminar":
                    return RedirectToAction("Seminar");
                    break;
                case "EnterpriseCooperate":
                    return RedirectToAction("EnterpriseCooperate");
                    break;
                case "Others":
                    return RedirectToAction("Others");
                    break;
            }
            return RedirectToAction("/Views/Activities/DeleteActivity.cshtml", activityDetails);
        }
    }

}
