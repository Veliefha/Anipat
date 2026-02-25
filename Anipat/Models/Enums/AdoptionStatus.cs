namespace Anipat.Models.Enums
{
    public enum AdoptionStatus
    {
        Pending = 1,   // Gözləmədə (İlk müraciət)
        Approved = 2,  // Təsdiqləndi (Admin qəbul etdi)
        Rejected = 3,  // Rədd edildi
        Completed = 4  // Tamamlandı (Heyvan artıq yeni evinə qovuşdu)
    }
}