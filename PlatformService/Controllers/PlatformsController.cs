using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo platformRepository;
        private readonly IMapper mapper;

        public PlatformsController(IPlatformRepo platformReporsitory, IMapper mapper)
        {
            this.platformRepository = platformReporsitory;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platForms = platformRepository.GetAllPlatforms();

            return Ok(mapper.Map<IEnumerable<Platform>,IEnumerable<PlatformReadDto>>(platForms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = platformRepository.GetPlatformById(id);

            if(platform != null)
            {
            return Ok(mapper.Map<Platform,PlatformReadDto>(platform));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = mapper.Map<Platform>(platformCreateDto);

            platformRepository.CreatePlatform(platform);
            var isCreated = platformRepository.SaveChanges();

            if(isCreated)
            {
                var platformReadDto = mapper.Map<PlatformReadDto>(platform);

                return CreatedAtRoute("GetPlatformById", new {id = platformReadDto.Id}, platformReadDto);
            }

            return NoContent();


        }
       
    }
}