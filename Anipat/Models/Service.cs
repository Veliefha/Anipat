using Anipat.Models.Base;

namespace Anipat.Models
{
    public class Service : BaseEntity
    {
        public string Name {  get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
