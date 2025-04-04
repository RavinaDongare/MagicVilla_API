
using System.ComponentModel.DataAnnotations;

namespace MagicVilla.Models.DTO
{
    public class VillaDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
       
    }
}
