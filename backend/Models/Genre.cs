﻿using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Genre
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<Game> Games { get; set; }
}
