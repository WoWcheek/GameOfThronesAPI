using DAL.Models.Locations;
using DAL.Models.LivingCreatures;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage;

public partial class GoTDBContext : DbContext
{
    public GoTDBContext(DbContextOptions<GoTDBContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        DataSeeding(builder);
    }

    private void DataSeeding(ModelBuilder builder)
    {
        var genders = new List<Gender>()
        {
            new()
            {
                Id = new Guid("1fc39009-6e3f-4415-89e5-1db92c9f45ee"),
                Name = "Male"
            },
            new()
            {
                Id = new Guid("b83f5072-0370-423f-90e5-c959b3f8e93c"),
                Name = "Female"
            },
            new()
            {
                Id = new Guid("21d3630a-4d0a-4d19-b41a-d4715ef7200e"),
                Name = "Other"
            }
        };
        builder.Entity<Gender>().HasData(genders);

        var petTypes = new List<PetType>()
        {
            new PetType
            {
                Id = new Guid("724ea49b-2ebf-422e-b2d0-5754b6c3bc56"),
                Name = "Dragon"
            },
            new PetType
            {
                Id = new Guid("819f58c8-c2bb-457f-9df8-9594f707b722"),
                Name = "Direwolf"
            }
        };
        builder.Entity<PetType>().HasData(petTypes);

        var locationTypes = new List<LocationType>()
        {
            new LocationType
            {
                Id = new Guid("a48783bc-f035-4104-a104-9f3c94d6c777"),
                Name = "Continent"
            },
            new LocationType
            {
                Id = new Guid("6e32994a-b21e-43db-8070-83c7be8e29e0"),
                Name = "Region"
            },
            new LocationType
            {
                Id = new Guid("0b25bad1-8ccd-4538-92ab-8223e409633b"),
                Name = "Estate"
            }
        };
        builder.Entity<LocationType>().HasData(locationTypes);
    }
}
