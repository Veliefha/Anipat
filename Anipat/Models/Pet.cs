namespace anipat.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string City { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Şəkil linki üçün yeni sahə (Sual işarəsi şəkilin məcburi olmadığını bildirir)
        public string? ImageUrl { get; set; }

        public string EnergyLevel { get; set; } = "Orta";
        public bool KidsFriendly { get; set; }
        public bool IsAdopted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}