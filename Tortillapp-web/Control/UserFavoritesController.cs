using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using Tortillapp_web.Data;
using Tortillapp_web.Models;
using static System.Formats.Asn1.AsnWriter;

namespace Tortillapp_web.Control
{
    public class UserFavoritesController : Controller
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public UserFavoritesController(tortillaContext context)
        {
            _context = context;
        }

        public UserFavorite UserFavorites { get; set; } = default!;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<UserFavorite> Add(ushort id_recipe, ushort id_user)
        {
            var recipe = await _context.RecipeInfos.FindAsync(id_recipe);
            var user = await _context.RecipeInfos.FindAsync(id_user);
            
            if (user != null && recipe != null)
            {
                _context.UserFavorites.Add(new UserFavorite
                {
                    UserId = user.UserId,
                    RecipeId = id_recipe,
                    UserAdded = DateTime.Now
                });
            }
            await _context.SaveChangesAsync();

            var favorite = await _context.UserFavorites.FindAsync(id_recipe, id_user);
            return favorite;
        }

        [HttpPost]
        public async Task<UserFavorite> Remove(ushort id_recipe, ushort id_user)
        {
            var favorite = await _context.UserFavorites.FindAsync(id_recipe, id_user);
            UserFavorites = favorite;
           _context.UserFavorites.Remove(UserFavorites);
            
            await _context.SaveChangesAsync();

            return favorite;
        }

    }
}
