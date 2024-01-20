using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
     [Route("api/[controller]")]
    [ApiController]
    // [Authorize] //If the request not authenticated it'll blocked (Jwt)
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(
            IRegionRepository regionRepository,
            IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllAsync()
        {
            //Get Data from database - Domain Models
            var regionsDomain = await _regionRepository.GetAllAsync();

            //return DTOs  
            return Ok(_mapper.Map<List<RegionDto>>(regionsDomain));
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Reader")]
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
        [ValidateModel]
        [Authorize(Roles = "OmerFaruk")]
        public async Task<IActionResult> Create([FromBody] CreateRegionRequestDto createRegionRequestDto)
        {
            //If model state is not valid it sends bad request error

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
        [ValidateModel]
        [Authorize(Roles = "OmerFaruk")]
        public async Task<IActionResult> Update([FromRoute] Guid Id,
            [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

            //Check if region exists
            if (regionDomainModel == null)
                return NotFound();
            
            await _regionRepository.UpdateAsync(Id, regionDomainModel);

            
            //We never back Domain models. We always back DTOs to client
            //Map Domain Model Back To DTO

            //Return to Swagger to show it
            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "OmerFaruk")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid Id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(Id);

            if (regionDomainModel == null)
                return NotFound();

            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}