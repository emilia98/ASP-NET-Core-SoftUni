using System;
using System.ComponentModel.DataAnnotations;

namespace HomeForMe.Data.Common.Models
{
    public class BaseModel<TKey> : IAuditInfo
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime AddedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
