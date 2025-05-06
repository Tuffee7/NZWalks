using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKms { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DifficultyID { get; set; }

        public Guid RegionID { get; set; }


        public AddWalkRequestDto()
        {

        }
    }
}
