using System;

namespace HomeForMe.Data.Common.Models
{
    public interface IAuditInfo
    {
        DateTime AddedAt { get; set; }

        DateTime? UpdatedAt { get; set; }
    }
}
