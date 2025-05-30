﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ArtVista.Domain.Entities
{
    public class Gallery
    {
        public int GalleryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [Required]
        public string ArtistId { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public Artist Artist { get; set; }
        public ICollection<ArtworkGallery> ArtworkGalleries { get; set; } = new List<ArtworkGallery>();
    }
}
