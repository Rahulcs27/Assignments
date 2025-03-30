using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace ArtVista.Domain.Entities
{
    public class Artwork
    {
        [Key]
        public int ArtworkID { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }

        [Required]
        public string ImageURL { get; set; } = string.Empty;
        
        public string ArtistID { get; set; } 

        [ForeignKey("ArtistID")]
        [JsonIgnore]
        public virtual Artist? Artist { get; set; }

        public virtual ICollection<ArtworkGallery> ArtworkGalleries { get; set; } = new List<ArtworkGallery>();

        public virtual ICollection<FavoriteArtwork> FavoriteArtworks { get; set; } = new List<FavoriteArtwork>();
    }
}
