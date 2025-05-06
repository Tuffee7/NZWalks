using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        { 

        }


        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties
            // Easy, Medium, High
            List<Difficulty> difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    ID = Guid.Parse("4e6fbcd3-ea43-4cc9-b071-55763bff7ca2"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    ID = Guid.Parse("1519fde1-81aa-44a2-a4e8-32d6d3e972a6"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    ID = Guid.Parse("4c51936e-c50d-45c1-b9cd-9565d74d3a88"),
                    Name = "Hard"
                }
            };
            // Seed difficulties to DB
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            // Seed data for regions
            List<Region> regions = new List<Region>()
            {
                new Region()
                {
                    ID = Guid.Parse("3fad7647-d22f-4d52-aade-b26c506ec4f8"),
                    Code = "AKL",
                    Name = "Auckland",
                    RegionImageUrl = "https://www.pexels.com/photo/two-people-walking-in-narrow-pathway-beside-buildings-while-holding-umbrella-1730847/"
                },
                new Region
                {
                    ID = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    ID = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    ID = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    ID = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    ID = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250264"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };
            // Seed regions to DB
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
