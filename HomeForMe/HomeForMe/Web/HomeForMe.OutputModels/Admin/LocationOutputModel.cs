using HomeForMe.Data.Models;
using HomeForMe.Services.Mapping;
using System;

namespace HomeForMe.OutputModels.Admin
{
    public class LocationOutputModel : IMapFrom<Location>
    {
        public string City { get; set; }

        public DateTime AddedAt { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
