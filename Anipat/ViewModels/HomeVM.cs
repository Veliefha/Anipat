using Anipat.Models;

namespace Anipat.ViewModels
{
    public class HomeVM
    {
        public List<Service> Services { get; set; }
        public List<Statistic> Statistics { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Team> Teams { get; set; }
        public List<Pet> LatestPets { get; set; }
    }
}
