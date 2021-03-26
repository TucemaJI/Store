using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
