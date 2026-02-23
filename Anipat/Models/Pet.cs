using Anipat.Models.Base;
using Microsoft.AspNetCore.Http; // Əlavə et
using System.ComponentModel.DataAnnotations.Schema; // Əlavə et

namespace Anipat.Models
{
    public class Pet : BaseEntity
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped] // SQL-də sütun yaratma, ancaq faylı tutmaq üçündür
        public IFormFile ImageFile { get; set; }

        public EnergyLevel Energy { get; set; }
        public bool IsKidsFriendly { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public bool IsAdopted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum EnergyLevel { Low, Medium, High }
}