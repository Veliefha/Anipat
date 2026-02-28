using Anipat.Models;

namespace Anipat.Models.ViewModels
{
    public class AdminDashboardVM
    {
        // Yuxarıdakı rəqəmlər üçün
        public int TodayAdoptionsCount { get; set; }
        public int TotalPetsCount { get; set; }
        public int PendingAppointmentsCount { get; set; }

        // Aşağıdakı cədvəl (siyahı) üçün
        public List<AdoptionRequest> LatestRequests { get; set; }
    }
}