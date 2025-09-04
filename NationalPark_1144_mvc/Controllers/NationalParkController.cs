using Microsoft.AspNetCore.Mvc;
using NationalPark_1144_mvc.Models;
using NationalPark_1144_mvc.Repository.IRepository;

namespace NationalPark_1144_mvc.Controllers
{
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        public NationalParkController(INationalParkRepository nationalParkRepository)
        {
            _nationalParkRepository = nationalParkRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allNationalParks = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);
            return Json(new { data = allNationalParks });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _nationalParkRepository.DeleteAsync(SD.NationalParkAPIPath, id);
            if (status)
                return Json(new { success = true, message = "Delete Successful" });
            return Json(new { success = false, message = "Delete Failed" });
        }
        #endregion
        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark nationalPark = new NationalPark();
            if (id == null || id == 0)
                return View(nationalPark);
            nationalPark = await _nationalParkRepository.GetAsync(SD.NationalParkAPIPath, id.GetValueOrDefault());
            if (nationalPark == null) return NotFound();
            return View(nationalPark);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NationalPark nationalPark)
        {
            if (nationalPark == null) return BadRequest();
            if (!ModelState.IsValid) return View(nationalPark);
            //***
            var files = HttpContext.Request.Form.Files;
            if (files.Count() > 0)
            {
                byte[] p1 = null;
                using (var fs = files[0].OpenReadStream())
                {
                    using (var ms = new MemoryStream())
                    {
                        fs.CopyTo(ms);
                        p1 = ms.ToArray();
                    }
                }
                nationalPark.Picture = p1;
            }
            else
            {
                var objFromDb = await _nationalParkRepository.GetAsync(SD.NationalParkAPIPath, nationalPark.Id);
                nationalPark.Picture = objFromDb.Picture;   
            }
            //****
            if (nationalPark.Id == 0)
                await _nationalParkRepository.CreateAsync(SD.NationalParkAPIPath, nationalPark);
            else
                await _nationalParkRepository.UpdateAsync(SD.NationalParkAPIPath , nationalPark);
            return RedirectToAction("Index");
        }
    }
}