using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        //Create Walk
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateWalksRequestDto walksRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var walkDomainModel = _mapper.Map<Walk>(walksRequestDto);
            await _walkRepository.CreateAsync(walkDomainModel);

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            var walksDomainModel = await _walkRepository.GetAllAsync(filterOn, filterQuery);

            //Map Domain Model to Dto
            return Ok(_mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetWalkByIdAsync([FromRoute] Guid Id)
        {
            var walksDomainModel = await _walkRepository.GetWalkByIdAsync(Id);

            if (walksDomainModel != null)
                return Ok(_mapper.Map<WalkDto>(walksDomainModel));

            return NotFound();
        }


        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid Id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //Map DTO to Domain model
            var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await _walkRepository.UpdateAsync(Id, walkDomainModel);

            if (walkDomainModel != null)
                return Ok(_mapper.Map<WalkDto>(walkDomainModel));

            return NotFound();
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var deletedWalkDomainModel = await _walkRepository.DeleteAsync(Id);

            if (deletedWalkDomainModel != null)
                return Ok(_mapper.Map<WalkDto>(deletedWalkDomainModel));

            return NotFound();
        }
    }
}