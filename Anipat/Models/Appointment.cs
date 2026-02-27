using Anipat.Models.Base;
using Anipat.Models.Enums; // Statuslar üçün

namespace Anipat.Models
{
    public class Appointment : BaseEntity
    {
       

        // Kim görüş təyin edir?
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // Hansı tarix üçün?
        public DateTime AppointmentDate { get; set; }

        // Hansı saat aralığı (Slot)? Məs: "10:00 - 11:00"
        public string TimeSlot { get; set; }

        // Görüşün məqsədi (Sahiblənmə öncəsi tanışlıq, müayinə və s.)
        public string Subject { get; set; }

        // Adoption-dakı kimi Status (Pending, Approved, Rejected)
        public AdoptionStatus Status { get; set; } = AdoptionStatus.Pending;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}