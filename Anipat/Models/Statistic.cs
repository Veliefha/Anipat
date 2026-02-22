using Anipat.Models.Base;

namespace Anipat.Models
{
    public class Statistic : BaseEntity
    {
        public string IconPath { get; set; } 
        public int Count { get; set; }      
        public string Title { get; set; }    
    }
}
