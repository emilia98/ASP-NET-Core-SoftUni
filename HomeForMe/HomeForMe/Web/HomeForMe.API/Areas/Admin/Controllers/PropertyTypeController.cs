using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeForMe.API.Areas.Admin.Controllers
{
    public class PropertyTypeController : BaseAdminAPIController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new
            {
                PropertyType = "Reach the property type area!"
            });
        }
    }
}
