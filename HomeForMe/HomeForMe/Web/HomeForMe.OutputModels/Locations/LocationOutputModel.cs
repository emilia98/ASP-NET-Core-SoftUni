using HomeForMe.Data.Models;
using HomeForMe.Services.Mapping;

namespace HomeForMe.OutputModels.Locations
{
    public class LocationOutputModel : IMapFrom<Location>
    {
        public string City { get; set; }
    }
}
