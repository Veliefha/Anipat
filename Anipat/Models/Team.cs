using Anipat.Models.Base;

namespace Anipat.Models
{
    public class Team : BaseEntity
    {
        public string FullName { get; set; } 
        public string Position { get; set; } 
        public string ImageUrl { get; set; } 
    }
}
