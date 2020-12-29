using HomeForMe.Services.Data.Contracts;

namespace HomeForMe.API.Areas.Admin.Controllers
{
    public class LocationController : BaseAdminAPIController
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
    }
}
