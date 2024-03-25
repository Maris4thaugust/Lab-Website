using Microsoft.AspNetCore.Mvc;
using WebSEnR.Interface.NewsInterface;
using WebSEnR.Interface;
using WebSEnR.ViewModel.NewsViewModel;
using WebSEnR.Interface.NewsInterface;
using WebSEnR.Models;

namespace WebSEnR.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsContext;
        private readonly IPhotoService _photoService;

        public NewsController(INewsRepository newsContext, IPhotoService photoService)
        {
            _newsContext = newsContext;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<News> News = await _newsContext.GetAll();
            return View("/Views/News/Index.cshtml", News);
        }

        public async Task<IActionResult> ContractNews()
        {
            IEnumerable<News> News = await _newsContext.GetAll();
            return View("/Views/News/ContractNews.cshtml", News);
        }
        public async Task<IActionResult> ScholarshipNews()
        {
            IEnumerable<News> News = await _newsContext.GetAll();
            return View("/Views/News/ScholarshipNews.cshtml", News);
        }
        public async Task<IActionResult> OthersNews()
        {
            IEnumerable<News> News = await _newsContext.GetAll();
            return View("/Views/News/OthersNews.cshtml", News);
        }
        public IActionResult CreateNews()
        {
            return View("/Views/News/CreateNews.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> CreateNews(CreateNewsViewModel NewsVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(NewsVM.Image);
                var News = new News
                {
                    Title = NewsVM.Title,
                    Description = NewsVM.Description,
                    Image = result.Url.ToString(),
                    NewsCategory = NewsVM.NewsCategory,
                    Url = NewsVM.Url
                };
                _newsContext.Add(News);
                var Category = News.NewsCategory.ToString();
                switch (Category)
                {
                    case "SigningContract":
                        return RedirectToAction("ContractNews");
                        break;
                    case "Scholarship":
                        return RedirectToAction("ScholarshipNews");
                        break;
                    case "Others":
                        return RedirectToAction("OthersNews");
                        break;
                }
            }
            else
            {
                ModelState.AddModelError("", "Create Failed");
            }
            return View("/Views/News/CreateNews.cshtml", NewsVM);
        }
        public async Task<IActionResult> EditNews(int id)
        {
            var News = await _newsContext.GetByIdAsync(id);
            if (News == null) return View("Error!");
            var NewsVM = new EditNewsViewModel
            {
                Title = News.Title,
                Description = News.Description,
                NewsCategory = News.NewsCategory,
                Url = News.Url,
                ImageUrl = News.Image
            };
            return View("/Views/News/EditNews.cshtml", NewsVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditNews(int id, EditNewsViewModel NewsVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit News");
                return View("/Views/News/EditNews.cshtml", NewsVM);
            }
            var userRace = await _newsContext.GetByIdAsyncNoTracking(id);
            if (userRace != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userRace.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View("/Views/News/EditNews.cshtml", NewsVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(NewsVM.Image);
                var News = new News
                {
                    Id = id,
                    Title = NewsVM.Title,
                    Description = NewsVM.Description,
                    Image = photoResult.Url.ToString(),
                    Url = NewsVM.Url,
                    NewsCategory = NewsVM.NewsCategory
                };
                _newsContext.Update(News);
                var Category = News.NewsCategory.ToString();
                switch (Category)
                {
                    case "SigningContract":
                        return RedirectToAction("ContractNews");
                        break;
                    case "Scholarship":
                        return RedirectToAction("ScholarshipNews");
                        break;
                    case "Others":
                        return RedirectToAction("OthersNews");
                        break;
                }
            }
            else
            {
                ModelState.AddModelError("", "Edit Failed");
            }
            return View("/Views/News/EditNews.cshtml", NewsVM);
        }
        public async Task<IActionResult> DeleteNews(int id)
        {
            var NewsDetails = await _newsContext.GetByIdAsync(id);
            if (NewsDetails == null) return View("Error");
            return View("/Views/News/DeleteNews.cshtml", NewsDetails);
        }

        [HttpPost, ActionName("DeleteNews")]
        public async Task<IActionResult> Delete(int id)
        {
            var NewsDetails = await _newsContext.GetByIdAsync(id);
            if (NewsDetails == null) return View("Error");
            var Category = NewsDetails.NewsCategory.ToString(); //Take category to return the view
            _newsContext.Delete(NewsDetails);
            switch (Category)
            {
                case "SigningContract":
                    return RedirectToAction("ContractNews");
                    break;
                case "Scholarship":
                    return RedirectToAction("ScholarshipNews");
                    break;
                case "Others":
                    return RedirectToAction("OthersNews");
                    break;
            }
            return RedirectToAction("/Views/News/DeleteNews.cshtml", NewsDetails);
        }
    }
}

