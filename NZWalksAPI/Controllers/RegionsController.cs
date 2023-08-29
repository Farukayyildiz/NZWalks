using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalksDbContext _context;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        public RegionsController(
            NzWalksDbContext context,
            IRegionRepository regionRepository,
            IMapper mapper)
        {
            _context = context;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //Get Data from database - Domain Models
            var regionsDomain = await _regionRepository.GetAllAsync();

            //Map domain models to DTOs
            // var regionsDto = new List<RegionDto>();
            // foreach (var regionDomain in regionsDomain)
            // {
            //     regionsDto.Add(new RegionDto
            //     {
            //         Id = regionDomain.Id,
            //         Code = regionDomain.Code,
            //         Name = regionDomain.Name,
            //         RegionImageUrl = regionDomain.RegionImageUrl
            //     });
            // }

            //return DTOs  
            return Ok(_mapper.Map<List<RegionDto>>(regionsDomain));
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            // var region = _context.Regions.Find(Id); //just works with ID field. It is useless for other fields.!!

            //Get Region Domain Model From DB
            var regionDomain = await _regionRepository.GetByIdAsync(Id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Return DTO back to client
            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionRequestDto createRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = _mapper.Map<Region>(createRegionRequestDto);

            //Use Domain Model to Create Region
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            //It turns to information about newly saved item. And show it in the swagger.
            //Map Domain Model Back to DTO
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{Id:Guid}")] //only Guid type are passed. ( ' : '  for filtering)
        public async Task<IActionResult> Update([FromRoute] Guid Id,
            [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);
            
            
            //Check if region exists
            await _regionRepository.UpdateAsync(Id, regionDomainModel);
            if (regionDomainModel == null)
                return NotFound();

            //We never back Domain models. We always back DTOs to client
            //Map Domain Model Back To DTO

            //Return to Swagger to show it
            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid Id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(Id);

            if (regionDomainModel == null)
                return NotFound();
            
            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}