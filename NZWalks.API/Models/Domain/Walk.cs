namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKms { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DifficultyID { get; set; }

        public Guid RegionID { get; set; }


        // Navigation properties

        public Difficulty Difficulty { get; set; }

        public Region Region { get; set; }
    }
}
