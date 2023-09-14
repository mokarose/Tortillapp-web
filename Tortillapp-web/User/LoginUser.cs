using System;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFramework;
using NuGet.Packaging.Rules;
using Tortillapp_web.Model;

namespace Tortillapp_web
{
	public class LoginUser
	{
        private readonly Tortillapp_web.Data.tortillaContext _context;

        public LoginUser(Tortillapp_web.Data.tortillaContext context)
        {
            _context = context;
        }
        public UserData User { get; set; } = default!;

        
    }
}
