using DAL.Models.Houses;
using DAL.Models.Locations;
using DAL.Models.LivingCreatures;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storage;

public partial class GoTDBContext : DbContext
{
    public DbSet<Pet> Pets { get; set; }

    public DbSet<House> Houses { get; set; }

    public DbSet<Gender> Genders { get; set; }

    public DbSet<PetType> PetTypes { get; set; }

    public DbSet<Location> Locations { get; set; }

    public DbSet<Character> Characters { get; set; }

    public DbSet<LocationType> LocationTypes { get; set; }
}
