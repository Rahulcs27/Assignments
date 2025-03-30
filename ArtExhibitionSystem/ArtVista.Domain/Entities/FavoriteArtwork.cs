using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtVista.Domain.Entities
{
    public class FavoriteArtwork
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ArtworkID { get; set; }

        [ForeignKey("ArtworkID")]
        public virtual Artwork Artwork { get; set; }
    }
}
