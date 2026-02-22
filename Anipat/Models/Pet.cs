using Anipat.Models.Base;

namespace Anipat.Models
{
    public class Pet : BaseEntity
    {
       
        public string Name { get; set; }
        public string Breed { get; set; } 
        public int Age { get; set; }
        public string ImageUrl { get; set; }

        
        public EnergyLevel Energy { get; set; } 
        public bool IsKidsFriendly { get; set; }
        public string City { get; set; }

        public string Description { get; set; }
        public bool IsAdopted { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum EnergyLevel { Low, Medium, High }
}
