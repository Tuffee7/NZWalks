using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class InMemoryRegionRepository //: IRegionRepository
    {
        public InMemoryRegionRepository() 
        {

        }

        public Task<List<Region>> GetAllAsync()
        {
            List<Region> regionsDomain = new List<Region>
            {
                new Region
                {
                    ID = Guid.NewGuid(),
                    Name = "Falana",
                    Code = "FLN",
                    RegionImageUrl = "https://www.pexels.com/photo/two-people-walking-in-narrow-pathway-beside-buildings-while-holding-umbrella-1730847/"
                },
                new Region
                {
                    ID = Guid.NewGuid(),
                    Name = "Dhikna",
                    Code = "DKN",
                    RegionImageUrl = "https://www.pexels.com/photo/two-people-walking-in-narrow-pathway-beside-buildings-while-holding-umbrella-1730847/"
                }
            };

            return Task.FromResult(regionsDomain);
        }
    }
}
