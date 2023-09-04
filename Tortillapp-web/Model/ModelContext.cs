using System;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFramework;
using NuGet.Packaging.Rules;
using Tortillapp_web.Model;

namespace Tortillapp_web.Data
{
	public class ModelContext : DbContext
	{
		/*public ModelContext(DbContextOptions<ModelContext> options)
			: base(options)
		{
		}*/
		public DbSet<UserData> User_Data { get; set; } //= default!;

		public DbSet<UserRoles> User_Roles { get; set; } //= default!;
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySQL("server = localhost; port = 3306; database = tortilla; user = root; password = Lord0Rings;");
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<UserData>( uid =>
			{
				uid.HasKey(u => u.user_id);
				uid.Property(u => u.user_mail).IsRequired();
				uid.Property(u => u.user_name).IsRequired();
				uid.Property(u => u.user_pass).IsRequired();
				uid.HasOne(l => l.UserRole);
				uid.Property(u => u.show_name);
				uid.Property(u => u.show_pic);
				uid.Property(u => u.remember_me);
			});

			modelBuilder.Entity<UserRoles>(rid =>
			{
				rid.HasKey(r => r.role_id);
				rid.Property(r => r.role_name).IsRequired();
			});
		}
	}
}
