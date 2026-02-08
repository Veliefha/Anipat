using Anipat.Models.Base;

namespace Anipat.Models
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
    }
}
