using HomeForMe.Data.Models;
using HomeForMe.InputModels.Admin;
using HomeForMe.OutputModels.Admin;
using HomeForMe.Services.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeForMe.API.Areas.Admin.Controllers
{
    public class LocationController : BaseAdminAPIController
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _locationService.GetAll<Location>(true);

            return Ok(new
            {
                Locations = locations
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var location = await _locationService.GetById<LocationOutputModel>(id, true);

            if (location == null)
            {
                return NotFound(new
                {
                    Message = "Location does not exist!",
                    HasError = true
                });
            }

            return Ok(new
            {
                Location = location
            });
        }

        [HttpPost("new")]
        public async Task<IActionResult> New(LocationInputModel locationInputModel)
        {
            var location = new Location
            {
                City = locationInputModel.City
            };

            try
            {
                await _locationService.Create(location);
            }
            catch
            {
                return BadRequest(new
                {
                    Message = "An error occurred while adding a new location!",
                    HasError = true
                });
            
            }

            return Ok(new
            {
                Message = "Successfully added a new location!",
                HasSuccess = true
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _locationService.GetById<Location>(id, true);

            if (location == null)
            {
                return NotFound(new
                {
                    Message = "Location does not exist!",
                    HasError = true
                });
            }

            var action = "deleting";

            try
            {
                if (location.IsDeleted)
                {
                    await _locationService.Undelete(location);
                    action = "removing from deleted!";
                }
                else
                {
                    await _locationService.Delete(location);
                }
            }
            catch
            {
                return BadRequest(new
                {
                    Message = $"An error occurred while {action} a location!",
                    HasError = true
                });
            }

            return Ok(new
            {
                Message = $"Successfully {action} a location!",
                HasSuccess = true
            });
        }

        //TODO: Add update action
    }
}
