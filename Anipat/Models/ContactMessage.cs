using System.ComponentModel.DataAnnotations;

namespace Anipat.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Adınızı daxil etməyiniz mütləqdir.")]
        [StringLength(50, ErrorMessage = "Ad 50 simvoldan çox ola bilməz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email ünvanınızı yazın.")]
        [EmailAddress(ErrorMessage = "Zəhmət olmasa, düzgün bir email ünvanı daxil edin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mesaj hissəsi boş qala bilməz.")]
        [MinLength(10, ErrorMessage = "Mesajınız ən azı 10 simvol olmalıdır.")]
        public string Message { get; set; }

        public bool IsRead { get; set; } = false;
        public string? AdminReply { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}