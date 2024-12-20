using magazun.Data;
using Microsoft.EntityFrameworkCore;

namespace magazun
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// ����������� ShopDbContext � ���������� ������������
			builder.Services.AddDbContext<ShopDbContext>(options =>
				options.UseSqlite("Data Source=Shop.db"));

			// ����������� Database � ������������ ShopDbContext
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

				// ���������� ��������, ���� ��� ����
				dbContext.Database.Migrate();

				// ������������� ���� ������ � ��������� �������
				//ShopDbContext.InitializeDatabase(dbContext);
				dbContext.InitializeDatabase();

			}

			app.Run();
		}
	}
}
