using Store.BusinessLogic.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class BlockModel : BaseModel
    {
        [Required]
        new public string Id { get; set; }
        [Required]
        public bool Block { get; set; }
    }
}
