using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public RegionsController(NzWalksDbContext context,IRegionRepository regionRepository)
        {
            _context = context;
            _regionRepository = regionRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //Get Data from database - Domain Models
            var regionsDomain = await _regionRepository.GetAllAsync();

            //Map domain models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            //return DTOs  
            return Ok(regionsDomain);
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

            //Map Region Domain Model To Region DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            //Return DTO back to client
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionRequestDto createRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = createRegionRequestDto.Code,
                Name = createRegionRequestDto.Name,
                RegionImageUrl = createRegionRequestDto.RegionImageUrl
            };

            //Use Domain Model to Create Region
            await _context.Regions.AddAsync(regionDomainModel);
            await _context.SaveChangesAsync();

            //It turns to information about newly saved item. And show it in the swagger.
            //Map Domain Model Back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{Id:Guid}")] //only Guid type are passed. ( ' : '  for filtering)
        public async Task<IActionResult> Update([FromRoute] Guid Id,
            [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if region exists
            var regionDomainModel = await _context.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (regionDomainModel == null)
                return NotFound();

            //Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await _context.SaveChangesAsync();

            //We never back Domain models. We always back DTOs to client
            //Map Domain Model Back To DTO 
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            //Return to Swagger to show it
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid Id)
        {
            var regionDomainModel = await _context.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (regionDomainModel == null)
                return NotFound();

            _context.Regions.Remove(regionDomainModel); //There is no async delete method. It is still sync.
             await _context.SaveChangesAsync();

            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }
    }
}