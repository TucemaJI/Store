using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models
{
    public class OrderPayModel
    {
        [Required, StringLength(16)]
        public string Cardnumber { get; set; }
        [Required, MinLength(0), MaxLength(12)]
        public long Month { get; set; }
        [Required, MinLength(2000), MaxLength(2099)]
        public long Year { get; set; }
        [Required, MinLength(0), MaxLength(999)]
        public long Cvc { get; set; }
        [Required, MinLength(1)]
        public long Value { get; set; }
        [Required, MinLength(0)]
        public long OrderId { get; set; }
    }
}
