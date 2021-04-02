using Store.BusinessLogic.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models
{
    public class OrderPayModel : BaseModel
    {
        [Required, StringLength(16)]
        public string Cardnumber { get; set; }
        [Required]
        public long Month { get; set; }
        [Required]
        public long Year { get; set; }
        [Required]
        public long Cvc { get; set; }
        [Required]
        public long Value { get; set; }
        [Required]
        public long OrderId { get; set; }
    }
}
