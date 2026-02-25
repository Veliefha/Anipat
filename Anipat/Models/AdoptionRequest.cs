using Anipat.Models;
using Anipat.Models.Base;
using Anipat.Models.Enums;

public class AdoptionRequest : BaseEntity
{
    public int PetId { get; set; }
    public Pet Pet { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public string Message { get; set; } // İstifadəçinin qeydi
    public DateTime RequestDate { get; set; } = DateTime.Now;

    // Workflow-un əsası:
    public AdoptionStatus Status { get; set; } = AdoptionStatus.Pending;
}