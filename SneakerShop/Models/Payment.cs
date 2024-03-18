using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerShop.Models
{
    public class Payment
    {
        [Key]
        public int paymentId { get; set; }

        [ForeignKey("UserId")]
        public string? UserId {  get; set; }
        [Required(ErrorMessage = "Credit Number is required")]
        public string creditNum { get; set; }
        [Required(ErrorMessage = "Credit CVV is required")]
        public string creditCVV { get; set; }
        [Required(ErrorMessage = "Credit expairation date is required")]
        public DateTime creditDate { get; set; }
    }
}
