using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtVista.Application.DTOs
{
    public class ArtistDTO
    {
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}
