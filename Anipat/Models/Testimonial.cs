using Anipat.Models.Base;

namespace Anipat.Models
{
    public class Testimonial : BaseEntity
    {
        
        public string ClientName { get; set; }   
        public string ClientJob { get; set; }    
        public string Content { get; set; }     
        public string ClientImageUrl { get; set; }
    }
}
