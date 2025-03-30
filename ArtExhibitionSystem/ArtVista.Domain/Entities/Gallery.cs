using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtVista.Domain.Entities
{
    public class Gallery
    {
        [Key]
        public int GalleryID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? Location { get; set; }

        public string ArtistID { get; set; }
        [ForeignKey("ArtistID")]
        public virtual Artist Artist { get; set; }

        public virtual ICollection<ArtworkGallery> ArtworkGalleries { get; set; } = new List<ArtworkGallery>();
    }
}
