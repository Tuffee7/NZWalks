using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    // https://localhost:7238/api/regionsDomain
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// RESTful URL
        /// GET: https://localhost:7238/api/Regions
        /// 
        /// Get All Regions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            //List<Region> regionsDomain = new List<Region>
            //{
            //    new Region
            //    {
            //        ID = Guid.NewGuid(),
            //        Name = "Auckland",
            //        Code = "AKL",
            //        RegionImageUrl = "https://www.pexels.com/photo/two-people-walking-in-narrow-pathway-beside-buildings-while-holding-umbrella-1730847/"
            //    },
            //    new Region
            //    {
            //        ID = Guid.NewGuid(),
            //        Name = "Falana",
            //        Code = "FLN",
            //        RegionImageUrl = "https://www.pexels.com/photo/two-people-walking-in-narrow-pathway-beside-buildings-while-holding-umbrella-1730847/"
            //    }
            //};

            // Getting Domain Data from Database into our main Region Domain model class
            List<Region> regionsDomain = dbContext.Regions.ToList();

            // Mapping Domain model to Dto
            var regionsDto = new List<RegionDTO>();

            foreach (var region in regionsDomain) 
            {
                regionsDto.Add(new RegionDTO
                {
                    ID = region.ID,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

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
        public IActionResult GetRegionByID([FromRoute] Guid id)
        {
            // Here find method only uses the Primary key
            //var region = dbContext.Regions.Find(id);

            // First or Default method can also take other columns which are 
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.ID == id);

            if (regionDomain == null)
                return NotFound();
            
            // Map Region Domain model to Region DTO
            var regionsDto = new RegionDTO();

            regionsDto = new RegionDTO
            {
                ID = regionDomain.ID,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionsDto);

        }


        // POST: To create a new region by a client
        // POST: https://localhost:7238/api/Regions/
        [HttpPost]
        public IActionResult CreateRegion([FromBody]AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map DTO to Domain Model
            Region regionDomain = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            // Use Domain model to create Region
            dbContext.Regions.Add(regionDomain);
            dbContext.SaveChanges();

            // Map Domain model back to DTO
            RegionDTO regionDTO = new RegionDTO
            {
                ID = regionDomain.ID,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionByID), new { id = regionDTO.ID}, regionDTO);
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
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody]UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // Check if region exist in DB
            var updateRegionDomain = dbContext.Regions.FirstOrDefault(x => x.ID == id);

            // Return not found if region does not exist.
            if (updateRegionDomain == null)
                return NotFound();

            // Mapping DTO to Domain model 
            updateRegionDomain.Code = updateRegionRequestDTO.Code;
            updateRegionDomain.Name = updateRegionRequestDTO.Name;
            updateRegionDomain.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            // Saving Domain into DB to update DB via Entity Framework.
            dbContext.SaveChanges();

            // Mapping domain model to DTO
            var regionRequestDTO = new RegionDTO
            {
                ID = updateRegionDomain.ID,
                Code = updateRegionDomain.Code,
                Name = updateRegionDomain.Name,
                RegionImageUrl = updateRegionDomain.RegionImageUrl
            };

            return Ok(regionRequestDTO);
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
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            // Check if region exist in DB
            var deleteRegionDomain = dbContext.Regions.FirstOrDefault(x => x.ID == id);

            // Return not found if region does not exist.
            if (deleteRegionDomain == null)
                return NotFound();

            // Remove will delete the Region from Domain model
            dbContext.Regions.Remove(deleteRegionDomain);
            // Saving will save changes to DB. If it is removed from domain model and not Saved then it won't delete from DB
            // and hence restarting APP will once again show the region.
            dbContext.SaveChanges();

            var regionRequestDTO = new RegionDTO
            {
                ID = deleteRegionDomain.ID,
                Code = deleteRegionDomain.Code,
                Name = deleteRegionDomain.Name,
                RegionImageUrl = deleteRegionDomain.RegionImageUrl
            };

            return Ok(regionRequestDTO);
        }

    }
}
