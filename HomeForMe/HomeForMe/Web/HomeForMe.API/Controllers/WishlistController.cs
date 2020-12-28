using HomeForMe.Data;
using HomeForMe.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeForMe.API.Controllers
{
    [Authorize]
    public class WishlistController : BaseAPIController
    {
        private readonly ApplicationDbContext _dbContext;

        public WishlistController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("{propertyId}")]
        public async Task<IActionResult> Add(int propertyId)
        {
            var property = await _dbContext.Properties.FirstOrDefaultAsync(x => x.Id == x.Id);

            if (property == null)
            {
                return NotFound(new
                {
                    Message = "Property does not exist!",
                    HasError = true
                });
            }

            var user = await this.GetUser();

            if (property.UserId == user.Id)
            {
                return BadRequest(new
                {
                    Message = "Cannot add to wishlist your own property!",
                    HasError = true
                });
            }

            var wishlist = new Wishlist
            {
                PropertyId = propertyId,
                UserId = user.Id
            };
            
            try
            {
                await _dbContext.Wishlists.AddAsync(wishlist);
            }
            catch
            {
                return BadRequest(new
                {
                    Message = "An error occurred while adding property to wishlist!",
                    HasError = true
                });
            }

            return Ok(new
            {
                Message = "Successfully added property to wishlist!",
                HasSuccess = true
            });
        }

        [HttpDelete("{wishlistId}")]
        public async Task<IActionResult> Remove(int wishlistId)
        {
            var wishlist = await _dbContext.Wishlists.FirstOrDefaultAsync(x => x.Id == wishlistId);

            if (wishlist == null)
            {
                return NotFound(new
                {
                    Message = "Wishlist does not exist!",
                    HasError = true
                });
            }

            var user = await this.GetUser();

            if (user == null || wishlist.UserId != user.Id)
            {
                return BadRequest(new
                {
                    Message = "Cannot remove property from wishlist!",
                    HasError = true
                });
            }

            try
            {
                _dbContext.Wishlists.Remove(wishlist);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return BadRequest(new
                {
                    Message = "An error occurred while removing property from wishlist!",
                    HasError = true
                });
            }

            return Ok(new
            {
                Message = "Successfully removed property from wishlist!",
                HasSuccess = true
            });
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMy()
        {
            var user = await this.GetUser();

            if (user == null)
            {
                return Unauthorized(new
                {
                    Message = "Cannot access wishlisted properties!",
                    HasError = true
                });
            }

            var wishlists = await _dbContext.Wishlists.Where(x => x.UserId == user.Id).Include(x => x.Property).ToListAsync();

            return Ok(new
            {
                Wishlists = wishlists
            });
        }

        [NonAction]
        private async Task<AppUser> GetUser()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}