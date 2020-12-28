using System;

namespace HomeForMe.Data.Common.Models
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedAt { get; set; }
    }
}
