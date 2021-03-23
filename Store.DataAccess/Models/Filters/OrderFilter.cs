using System.ComponentModel.DataAnnotations;
using static Store.Shared.Enums.Enums;

namespace Store.DataAccess.Models.Filters
{
    public class OrderFilter : BaseFilter
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public StatusType Status { get; set; }
    }
}
