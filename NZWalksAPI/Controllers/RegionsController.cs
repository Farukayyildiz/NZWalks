using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalksDbContext _context;

        public RegionsController(NzWalksDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from database - Domain Models
            var regionsDomain = await _context.Regions.ToListAsync();

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
        public IActionResult GetById([FromRoute] Guid Id)
        {
            // var region = _context.Regions.Find(Id); //just works with ID field. It is useless for other fields.!!

            //Get Region Domain Model From DB
            var regionDomain = _context.Regions.FirstOrDefault(x => x.Id == Id);

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
        public IActionResult Create([FromBody] CreateRegionRequestDto createRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = createRegionRequestDto.Code,
                Name = createRegionRequestDto.Name,
                RegionImageUrl = createRegionRequestDto.RegionImageUrl
            };

            //Use Domain Model to Create Region
            _context.Regions.Add(regionDomainModel);
            _context.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid Id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if region exists
            var regionDomainModel = _context.Regions.FirstOrDefault(x => x.Id == Id);
            if (regionDomainModel == null)
                return NotFound();

            //Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            _context.SaveChanges();

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
        public IActionResult DeleteById([FromRoute] Guid Id)
        {
            var regionDomainModel = _context.Regions.FirstOrDefault(x => x.Id == Id);
            
            if (regionDomainModel == null)
                return NotFound();

            _context.Regions.Remove(regionDomainModel);
            _context.SaveChanges();
            
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