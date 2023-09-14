using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tortillapp_web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private CConnection nConnect;
        public string itag => (string)TempData[nameof(itag)];

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
		}

		public async void OnGet()
        {
			
		}

        public IActionResult OnGetLogout() 
        {
            HttpContext.Session.Remove("Usuario");
            return RedirectToPage("Index");
        }
        
    }
}