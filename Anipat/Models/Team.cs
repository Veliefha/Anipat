using Anipat.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anipat.Models
{
    public class Team : BaseEntity
    {
        public string FullName { get; set; } 
        public string Position { get; set; } 
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
