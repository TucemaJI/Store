using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities.Base
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationData { get; set; }

        public bool IsRemoved { get; set; }

        public BaseEntity()
        {
            CreationData = DateTime.UtcNow;
        }
    }
}
