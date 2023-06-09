﻿
using System.ComponentModel.DataAnnotations;

namespace ArtClub.DataAccess.Model
{
    public class Resource : ModelEntity
    {

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string Availability { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
}
