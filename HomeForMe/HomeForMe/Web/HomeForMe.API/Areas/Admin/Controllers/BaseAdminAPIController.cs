using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeForMe.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]
    [Route("/admin/[controller]")]
    [Authorize(Policy = "RequireAdminRole")]
    public class BaseAdminAPIController : ControllerBase
    {
    }
}
