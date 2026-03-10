using Anipat.Models.Base;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anipat.Models
{
    public enum PetSpecies {
        İt,
        Pişik,
        Dovşan,
        Quş,
        Digər
    }

    public enum EnergyLevel {
        Aşağı,
        Orta,
        Yüksək
    }

    public class Pet : BaseEntity
    {
        public string Name { get; set; }
        public PetSpecies Species { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public EnergyLevel Energy { get; set; }
        public bool IsKidsFriendly { get; set; }
        public string City { get; set; } // Bakı, Gəncə və s.
        public string? Description { get; set; }
        public bool IsAdopted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}