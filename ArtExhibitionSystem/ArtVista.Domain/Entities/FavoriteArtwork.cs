using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
namespace ArtVista.Domain.Entities
{
    public class FavoriteArtwork
    {
        public string UserId { get; set; }

        public int ArtworkID { get; set; }

        [ForeignKey("ArtworkID")]
        public virtual Artwork Artwork { get; set; }
    }
}
