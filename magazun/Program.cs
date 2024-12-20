using magazun.Data;
using Microsoft.EntityFrameworkCore;

namespace magazun
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// –егистраци€ ShopDbContext в контейнере зависимостей
			builder.Services.AddDbContext<ShopDbContext>(options =>
				options.UseSqlite("Data Source=Shop.db"));

			// –егистраци€ Database с зависимостью ShopDbContext
			builder.Services.AddScoped<IDatabase, Database>();


			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				//pattern: "{controller=Customer}/{action=Index}/{id?}");
				pattern: "{controller=Product}/{action=Index_product}/{id?}");

			using (var scope = app.Services.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<ShopDbContext>();

				// ѕрименение миграций, если они есть
				dbContext.Database.Migrate();

				// »нициализаци€ базы данных с тестовыми данными
				//ShopDbContext.InitializeDatabase(dbContext);
				dbContext.InitializeDatabase();

			}

			app.Run();
		}
	}
}
