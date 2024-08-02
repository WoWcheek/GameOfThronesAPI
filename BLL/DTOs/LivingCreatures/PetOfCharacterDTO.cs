﻿namespace BLL.DTOs.LivingCreatures;

internal class PetOfCharacterDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string? Gender { get; set; }
}
