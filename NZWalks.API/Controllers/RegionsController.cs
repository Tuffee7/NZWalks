using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:7238/api/regionsDomain
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// RESTful URL
        /// GET: https://localhost:7238/api/Regions
        /// 
        /// Get All Regions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Getting Domain Data from Database into our main Region Domain model class
            List<Region> regionsDomain = await regionRepository.GetAllAsync();

            // Mapping Domain model to Dto
            List<RegionDto> regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regionsDto);
        }

        /// <summary>
        /// Get single region by ID
        /// 
        /// GET: https://localhost:7238/api/Regions/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionByID([FromRoute] Guid id)
        {
            // Here find method only uses the Primary key
            //var region = dbContext.Regions.Find(id);

            // First or Default method can also take other columns which are 
            var regionDomain = await regionRepository.GetRegionByIDAsync(id);

            if (regionDomain == null)
                return NotFound();
            
            // Map Region Domain model to Region DTO
            RegionDto regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }


        // POST: To create a new region by a client
        // POST: https://localhost:7238/api/Regions/
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody]AddRegionRequestDto addRegionRequestDTO)
        {
            // Map DTO to Domain Model
            Region regionDomain = mapper.Map<Region>(addRegionRequestDTO);

            // Use Domain model to create Region
            regionDomain =  await regionRepository.CreateRegionAsync(regionDomain);

            // Map Domain model back to DTO
            RegionDto regionDto = mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetRegionByID), new { id = regionDto.ID}, regionDto);
        }

        /// <summary>
        /// PUT: To update an existing region in Database
        /// 
        /// PUT: https://localhost:7238/api/Regions/id
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="updateRegionRequestDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody]UpdateRegionRequestDto updateRegionRequestDTO)
        {
            // Updating Region Domain through updateRegionRequestDTO from the Body via Auto mapper.
            Region updateRegionDomain = mapper.Map<Region>(updateRegionRequestDTO);

            updateRegionDomain = await regionRepository.UpdateRegionAsync(id, updateRegionDomain);

            // Return not found if region does not exist.
            if (updateRegionDomain == null)
                return NotFound();

            // Mapping Domain model to DTO
            RegionDto regionRequestDto = mapper.Map<RegionDto>(updateRegionDomain);

            return Ok(regionRequestDto);
        }

        /// <summary>
        /// To Delete a region
        /// 
        /// DELETE: https://localhost:7238/api/Regions/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var deleteRegionDomain = await regionRepository.DeleteRegionAsync(id);

            // Return not found if region does not exist.
            if (deleteRegionDomain == null)
                return NotFound();

            // Mapping Domain model to DTO
            RegionDto regionRequestDto = mapper.Map<RegionDto>(deleteRegionDomain);

            return Ok(regionRequestDto);
        }

    }
}
