using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using National_Park_1144.Model;
using National_Park_1144.Model.DTO;
using National_Park_1144.Repository.IRepository;

namespace National_Park_1144.Controllers
{
    [Route("api/trail")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly ITrailRepository _trailRepo;
        private readonly IMapper _mapper;
        public TrailController(ITrailRepository trailRepo, IMapper mapper)
        {
            _trailRepo = trailRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetTrails()
        {
            return Ok(_trailRepo.GetTrails().Select(_mapper.Map<Trail, TrailDTO>));
        }
        [HttpGet("{trailId:int}", Name = "GetTrail")]
        public IActionResult GetTrail(int trailId)
        {
            var trail = _trailRepo.GetTrail(trailId);
            if (trail == null)
            {
                return NotFound();
            }
            var trailDto = _mapper.Map<TrailDTO>(trail);
            return Ok(trailDto);
        }
        [HttpPost]
        public IActionResult CreateTrail([FromBody] TrailDTO trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_trailRepo.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail already exists");
                return BadRequest(ModelState);
            }
            var trail = _mapper.Map<TrailDTO, Trail>(trailDto);
            if (!_trailRepo.CreateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {trail.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrail", new { trailId = trail.Id }, trail);
        }
        [HttpPut]
        public IActionResult UpdateTrail([FromBody] TrailDTO trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_trailRepo.TrailExists(trailDto.Id))
            {
                return NotFound();
            }
            var trail = _mapper.Map<TrailDTO, Trail>(trailDto);
            if (!_trailRepo.UpdateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {trail.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{trailId:int}")]
        public IActionResult DeleteTrail(int trailId)
        {
            if (trailId == 0)
            {
                return BadRequest();
            }
            if (!_trailRepo.TrailExists(trailId))
            {
                return NotFound();
            }
            var trail = _trailRepo.GetTrail(trailId);
            if (!_trailRepo.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {trail.Name}");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
