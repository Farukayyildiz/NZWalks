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
            var walkDomainModel = _mapper.Map<Walk>(walksRequestDto);
            await _walkRepository.CreateAsync(walkDomainModel);

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }

        //BUG Test yapıldığında verilerin gelmediği gözlemlendi.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await _walkRepository.GetAllAsync();
            
            //Map Domain Model to Dto
            return Ok(_mapper.Map<List<WalkDto>>(walksDomainModel));
        }
    }
}
