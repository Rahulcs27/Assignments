﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtVista.Domain.Entities
{
    public class ArtworkGallery
    {
        [Key]
        public int Id { get; set; } 

        public int ArtworkID { get; set; }
        [ForeignKey("ArtworkID")]
        public virtual Artwork Artwork { get; set; }

        public int GalleryID { get; set; }
        [ForeignKey("GalleryID")]
        public virtual Gallery Gallery { get; set; }
    }
}
