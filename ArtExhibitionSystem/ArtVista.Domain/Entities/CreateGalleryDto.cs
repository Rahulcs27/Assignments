using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtVista.Domain.Entities
{
    public class CreateGalleryDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Location { get; set; }

        [Required]
        public string ArtistId { get; set; }
    }
}
