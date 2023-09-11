using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;
using Tortillapp_web.Model;

namespace Tortillapp_web.Pages.Users;

public class UserModel : PageModel
{

	private readonly Tortillapp_web.Data.tortillaContext _context;

	public UserModel(Tortillapp_web.Data.tortillaContext context)
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

