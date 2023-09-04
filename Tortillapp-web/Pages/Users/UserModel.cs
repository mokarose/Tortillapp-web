using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;
using Tortillapp_web.tortilla;

namespace Tortillapp_web.Pages.Users;

public class UserModel : PageModel
{

	private readonly Tortillapp_web.tortilla.tortillaContext _context;

	public UserModel(Tortillapp_web.tortilla.tortillaContext context)
	{
		_context = context;
	}

	public IList<UserData> UserData { get; set; } = default!;

	public async Task OnGetAsync()
	{
		if (_context.UserData != null)
		{
			UserData = await _context.UserData.ToListAsync();
		}

	}
}

