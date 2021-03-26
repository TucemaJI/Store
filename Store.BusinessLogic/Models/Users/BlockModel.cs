using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class BlockModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public bool Block { get; set; }
    }
}
