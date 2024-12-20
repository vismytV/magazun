using magazun.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace magazun.Data
{
	public class ShopDbContext : DbContext
	{

		//Добавляем конструктор в ShopDbContext, который принимает DbContextOptions<ShopDbContext>
		//и передает его в базовый конструктор DbContext
		public ShopDbContext(DbContextOptions<ShopDbContext> options)
		: base(options)
		{
		}

		public DbSet<Login> Logins { get; set; }

		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderProduct> OrderProducts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=ShopDatabase.db");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// зв'язки між таблицями
			modelBuilder.Entity<OrderProduct>()
				.HasKey(op => new { op.OrderId, op.ProductId });//складений первинний ключ таблиці OrderProduct

			modelBuilder.Entity<OrderProduct>()
				.HasOne(op => op.Order)// Кожен запис у OrderProduct пов'язаний з одним Order
				.WithMany(o => o.OrderProducts)//Кожен Order може мати багато записів у OrderProduct
				.HasForeignKey(op => op.OrderId);//Вказує, що поле OrderId у таблиці OrderProduct є зовнішнім ключем, який посилається на Order.OrderId

			modelBuilder.Entity<OrderProduct>()
				.HasOne(op => op.Product)//Кожен запис у OrderProduct пов'язаний з одним Product
				.WithMany(p => p.OrderProducts)//Кожен Product може бути присутнім у багатьох записах OrderProduct
				.HasForeignKey(op => op.ProductId);//Вказує, що поле ProductId у таблиці OrderProduct є зовнішнім ключем, який посилається на Product.ProductId
		}

		public void InitializeDatabase()
		{
			if (!Customers.Any())
			{
				Customers.AddRange(new List<Customer>
				{
					new Customer { CustomerId=1,FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
					new Customer { CustomerId=2,FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
				});
				
				SaveChanges();
			}

			if (!Products.Any())
			{
				Products.AddRange(new List<Product>
				{
					new Product {ProductId=1, Name = "Laptop", Price = 1200, Description = "High-performance laptop" },
					new Product {ProductId=2, Name = "Mouse", Price = 25, Description = "Wireless mouse" },
					new Product {ProductId=3, Name = "Keyboard", Price = 45, Description = "Mechanical keyboard" }
				});
				SaveChanges();
			}

			if (!Orders.Any())
			{
				Orders.Add(new Order
				{
					OrderDate = DateTime.Now,
					CustomerId = 1,
					TotalAmount = 1270,
					OrderProducts = new List<OrderProduct>
					{
						new OrderProduct { ProductId = 1 }, // Laptop
						new OrderProduct { ProductId = 2 }, // Mouse
					}
				});
				SaveChanges();
			}

			if (!Logins.Any())
			{
				Logins.AddRange(new List<Login>
				{
					new Login {Id=1,idCustomer=1, UserLogin = "admin", Password = "admin123", Role = "Admin",
						customer = Customers.FirstOrDefault(c => c.FirstName == "John") },
					new Login {Id=2,idCustomer=2, UserLogin = "user1", Password = "password123", Role = "User",
						customer = Customers.FirstOrDefault(c => c.FirstName == "Jane") }
				});
				SaveChanges();
			}

			
		


			this.Database.EnsureCreated();
		}

		



	}
}
