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
    public class Artist
    {
        [Key]
        public string ArtistID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();

        public virtual ICollection<Gallery> Galleries { get; set; } = new List<Gallery>();


    }
}
