using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NationalPark_1144_mvc.Models;
using NationalPark_1144_mvc.Models.ViewModel;
using NationalPark_1144_mvc.Repository.IRepository;

namespace NationalPark_1144_mvc.Controllers
{
    public class TrailController : Controller
    {
        private readonly ITrailRepository _trailRepository;
        private readonly INationalParkRepository _nationalParkRepository;
        public TrailController(ITrailRepository trailRepository, INationalParkRepository nationalParkRepository)
        {
            _nationalParkRepository = nationalParkRepository;
            _trailRepository = trailRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _trailRepository.GetAllAsync(SD.TrailsAPIPath) });
        }
        #endregion
        public async Task<IActionResult> Upsert(int? id)
        {
            var nationalParkList = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);
            TrailVm trailVm = new TrailVm()
            {
                NationalParkList = nationalParkList.Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Trail = new Trail()
            };
            if(id == null) return View(trailVm);
            trailVm.Trail = await _trailRepository.GetAsync(SD.TrailsAPIPath, id.GetValueOrDefault());
            if (trailVm.Trail == null) return NotFound();
            return View(trailVm);
        }

    }
}