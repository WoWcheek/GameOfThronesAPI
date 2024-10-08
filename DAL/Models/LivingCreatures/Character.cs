﻿using DAL.Models.Houses;

namespace DAL.Models.LivingCreatures;

public class Character
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? AlsoKnownAs { get; set; }

    public Guid GenderId { get; set; }

    public virtual Gender Gender { get; set; } = null!;

    public Guid HouseId { get; set; }

    public virtual House House { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Biography { get; set; }

    public string? Photo { get; set; }

    public List<Pet> Pets { get; set; } = new List<Pet>();
}
