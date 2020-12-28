using HomeForMe.Data;
using HomeForMe.Data.Models;
using HomeForMe.InputModels.Property;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeForMe.API.Controllers
{
    public class PropertyController : BaseAPIController
    {
        private readonly ApplicationDbContext _dbContext;

        public PropertyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var properties = await _dbContext.Properties.ToListAsync();

            return Ok(new
            {
                Properties = properties
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var property = await _dbContext.Properties.FindAsync(id);

            if (property == null)
            {
                return NotFound(new
                {
                    Message = "Property does not exist!",
                    HasError = true
                });
            }

            return Ok(new
            {
                Property = property
            });
        }


        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetAllByUser()
        {
            var username = this.User.FindFirst(ClaimTypes.Name)?.Value;

            var userByUsername = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (userByUsername == null)
            {
                return Unauthorized(new
                {
                    Message = "Something went wrong while fetching your properties!",
                    HasError = true
                });
            }

            var properties = await _dbContext.Properties.Where(x => x.UserId == userByUsername.Id).ToListAsync();

            return Ok(new
            {
                Properties = properties
            });
        }

        [HttpGet("new/data")]
        public async Task<IActionResult> GetNewPropertyInfo()
        {
            var locations = await _dbContext.Locations.ToListAsync();
            var propertyTypes = await _dbContext.PropertyTypes.ToListAsync();

            return Ok(new
            {
                Locations = locations,
                PropertyTypes = propertyTypes
            });
        }

        [Authorize]
        [HttpPost("new")]
        public async Task<IActionResult> New(NewPropertyInputModel propertyInputModel)
        {
            var locationId = propertyInputModel.Location;
            var location = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == locationId);

            if (location == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid location!",
                    HasFormError = true
                });
            }

            var typeId = propertyInputModel.Type;
            var propertyType = await _dbContext.PropertyTypes.FirstOrDefaultAsync(t => t.Id == typeId);

            if (propertyType == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid property type!",
                    HasFormError = true
                });
            }

            var user = await this.GetUser();

            if (user == null)
            {
                return Unauthorized(new
                {
                    Message = "Something went wrong while fetching your properties!",
                    HasError = true
                });
            }

            var property = new Property
            {
                LocationId = locationId.Value,
                PropertyTypeId = typeId.Value,
                UserId = user.Id,
                Price = propertyInputModel.Price.Value,
                Bedrooms = propertyInputModel.Bedrooms.Value,
                Bathrooms = propertyInputModel.Bathrooms.Value,
                Description = propertyInputModel.Description,
                AddedAt = DateTime.Now,
                ForRent = true
            };

            try
            {
                await _dbContext.Properties.AddAsync(property);
                await _dbContext.SaveChangesAsync();
            } 
            catch
            {
                return BadRequest(new
                {
                    Message = "An error occurred while trying to add new property!",
                    HasError = true
                });
            }

            return Ok(new
            {
                SuccessMessage = "Successfully added a new property!",
                HasSuccess = true
            });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var property = await _dbContext.Properties.FirstOrDefaultAsync(x => x.Id == id);

            if (property == null)
            {
                return NotFound(new
                {
                    Message = "Property does not exist!",
                    HasError = true
                });
            }

            var user = await this.GetUser();

            if (user == null || user.Id != property.UserId)
            {
                return Unauthorized(new
                {
                    Message = "Cannot process deleting a property!",
                    HasError = true
                });
            }

            try
            {
                _dbContext.Properties.Remove(property);
                await _dbContext.SaveChangesAsync();
            } 
            catch
            {
                return BadRequest(new
                {
                    Message = "Something went wrong while deleting a property!",
                    HasError = true
                });
            }

            return Ok(new
            {
                Message = "Successfully deleted a property!",
                HasSuccess = true
            });
        }

        [Authorize]
        [HttpGet("update/{id}")]
        public async Task<IActionResult> UpdateGet(int id)
        {
            var property = await _dbContext.Properties.FirstOrDefaultAsync(x => x.Id == id);

            if (property == null)
            {
                return NotFound(new
                {
                    Message = "Property does not exist!",
                    HasError = true
                });
            }

            var user = await this.GetUser();

            if (user == null || user.Id != property.UserId)
            {
                return Unauthorized(new
                {
                    Message = "Cannot process updating a property!",
                    HasError = true
                });
            }

            return Ok(new
            {
                Property = property
            });
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, NewPropertyInputModel propertyInputModel)
        {
            var property = await _dbContext.Properties.FirstOrDefaultAsync(x => x.Id == id);

            if (property == null)
            {
                return NotFound(new
                {
                    Message = "Property does not exist!",
                    HasError = true
                });
            }

            var user = await this.GetUser();

            if (user == null || user.Id != property.UserId)
            {
                return Unauthorized(new
                {
                    Message = "Cannot process updating a property!",
                    HasError = true
                });
            }

            var locationId = propertyInputModel.Location;
            var location = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == locationId);

            if (location == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid location!",
                    HasFormError = true
                });
            }

            var typeId = propertyInputModel.Type;
            var propertyType = await _dbContext.PropertyTypes.FirstOrDefaultAsync(t => t.Id == typeId);

            if (propertyType == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid property type!",
                    HasFormError = true
                });
            }

            property.Bathrooms = propertyInputModel.Bathrooms;
            property.Bedrooms = propertyInputModel.Bedrooms;
            property.Description = propertyInputModel.Description;
            property.LocationId = propertyInputModel.Location.Value;
            property.Price = propertyInputModel.Price.Value;
            property.PropertyTypeId = propertyInputModel.Type.Value;
            property.UpdatedAt = DateTime.Now;

            try
            {
                _dbContext.Properties.Update(property);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return BadRequest(new
                {
                    Message = "Something went wrong while updating a property!",
                    HasError = true
                });
            }

            return Ok(new
            {
                Message = "Successfully updating a property!",
                HasSuccess = true
            });
        }

        [NonAction]
        private async Task<AppUser> GetUser()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await this._dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
