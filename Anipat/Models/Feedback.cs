using Anipat.Models.Base;

namespace Anipat.Models
{
    public class Feedback : BaseEntity
    {
        public string Author {  get; set; }
        public string Position { get; set; }
        public string Text { get; set; }
        public string Image {  get; set; }
    }
}
