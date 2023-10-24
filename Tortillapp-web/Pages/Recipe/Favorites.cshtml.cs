using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Packaging;
using Org.BouncyCastle.Utilities.Collections;
using Tortillapp_web.Data;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Receta
{
    public class FavoritesModel : PageModel
    {
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public FavoritesModel(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }

        public IList<RecipeInfo> RecipeInfo { get; set; } = default!;
        public IList<Tag> Tags { get; set; } = default!;


        public async Task OnGetAsync()
        {
            if (_context.RecipeInfos != null)
            {
                RecipeInfo = await _context.RecipeInfos
                .Include(r => r.User).ToListAsync();
            }
        }
    }
}
