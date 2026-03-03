using Anipat.Models.Base;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anipat.Models
{
    // Heyvan növləri (Burada saxlayırıq, ayrı fayla ehtiyac yoxdur)
    public enum PetSpecies {
        [Display(Name = "İt")]
        Dog,
        [Display(Name = "Pişik")]
        Cat,
        [Display(Name = "Dovşan")]
        Rabbit,
        [Display(Name = "Quş")]
        Bird,
        [Display(Name = "Digər")]
        Other
    }

    // Enerji səviyyəsi
    public enum EnergyLevel { Low, Medium, High }

    public class Pet : BaseEntity
    {
        public string Name { get; set; }

        // Heyvanın növü
        public PetSpecies Species { get; set; }

        public string Breed { get; set; }
        public int Age { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public EnergyLevel Energy { get; set; }
        public bool IsKidsFriendly { get; set; }
        public string City { get; set; }
        public string? Description { get; set; }
        public bool IsAdopted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [NotMapped]
        public double Distance { get; set; }
    }
}