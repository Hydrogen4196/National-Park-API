using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using National_Park_1144.Model;
using National_Park_1144.Model.DTO;
using National_Park_1144.Repository.IRepository;

namespace National_Park_1144.Controllers
{
    [Route("api/NationalPark")]
    [ApiController]
    public class NationalParkController : Controller
    {
        private readonly INpRepository _npRepo;
        private readonly IMapper _mapper;
        public NationalParkController(INpRepository npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetNationalPark()
        {
            var nationalDto = _npRepo.GetNationalParks().Select(_mapper.Map<NationalPark, NpDTO>);
            return Ok(nationalDto);//200
        }
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalPark = _npRepo.GetNationalPark(nationalParkId);
            if (nationalPark == null)
            {
                return NotFound();
            }
            var nationalDto = _mapper.Map<NpDTO>(nationalPark);
            return Ok(nationalDto);
        }
        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NpDTO nationalParkDto)
        {
            if (nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_npRepo.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park already exists");
                return BadRequest(ModelState);
            }
            var nationalPark = _mapper.Map<NpDTO, NationalPark>(nationalParkDto);
            if (!_npRepo.CreateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetNationalPark", new { nationalParkId = nationalPark.Id }, nationalPark);
        }
        [HttpPut]
        public IActionResult UpdateNationalPark([FromBody] NpDTO npDTO)
        {
            if (npDTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var nationalPark = _mapper.Map<NationalPark>(npDTO);
            if (!_npRepo.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{nationalParkId:int}")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (nationalParkId == null) return BadRequest();
            if (!_npRepo.NationalParkExists(nationalParkId)) return NotFound();
            var nationalPark = _npRepo.GetNationalPark(nationalParkId);
            if (!_npRepo.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}