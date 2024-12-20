using magazun.Models;
using Microsoft.EntityFrameworkCore;

namespace magazun.Data
{
	public class Database : IDatabase
	{
		private readonly ShopDbContext _context;

		public Database(ShopDbContext context)
		{
			_context = context;
		}
		//покупатель+++++++++++++++++
		void IDatabase.AddCustomer(Customer customer)
		{
			throw new NotImplementedException();
		}

		void IDatabase.DeleteCustomer(int customerId)
		{
			throw new NotImplementedException();
		}

		void IDatabase.EditCustomer(Customer customer)
		{
			throw new NotImplementedException();
		}

		void IDatabase.UpdateCustomer(Customer customer)
		{
			throw new NotImplementedException();
		}

		void IDatabase.UpdateProduct(Product product)
		{
			throw new NotImplementedException();
		}
		List<Customer> IDatabase.GetCustomer()
		{
			return _context.Customers.ToList();
		}

		//product+++++++++++++++++++++++
		void IDatabase.DeleteProduct(int product)
		{
			throw new NotImplementedException();
		}

		void IDatabase.AddProduct(Product product)
		{
			throw new NotImplementedException();
		}

		void IDatabase.EditProduct(Product product)
		{
			throw new NotImplementedException();
		}

		public List<Product> GetProduct()
		{
			return _context.Products.ToList(); // Возвращаем все продукты из базы данных
		}
		
		//Order
		void IDatabase.AddOrder(Order order)
		{
			throw new NotImplementedException();
		}
		void IDatabase.DeleteOrder(int order)
		{
			throw new NotImplementedException();
		}

		void IDatabase.EditOrder(Order order)
		{
			throw new NotImplementedException();
		}

		
		public IQueryable<Order> GetOrder()
		{
			return _context.Orders
				.Include(o => o.OrderProducts) // Подключаем связанные OrderProducts
				.ThenInclude(op => op.Product); // Подключаем связанные продукты
		}


		void IDatabase.UpdateOrder(Order order)
		{
			throw new NotImplementedException();
		}

		List<Login> IDatabase.GetLogin()
		{
			return _context.Logins.ToList();
		}

		/*public Queryable<Login> GetLogin()
		{
			return _context.Logins
				.Include(c=>c.) // Подключаем связанные OrderProducts
				.ThenInclude(op => op.Product); // Подключаем связанные продукты
		}*/
		void IDatabase.AddLogin(Login login)
		{
			throw new NotImplementedException();
		}

		void IDatabase.DeleteLogin(int login)
		{
			throw new NotImplementedException();
		}

		void IDatabase.EditLogin(Login login)
		{
			throw new NotImplementedException();
		}

		void IDatabase.UpdateLogin(Login login)
		{
			throw new NotImplementedException();
		}

		List<OrderProduct> IDatabase.GetOrderProduct()
		{
			return _context.OrderProducts.ToList();
		}

		void IDatabase.AddOrderProduct(OrderProduct orderProduct)
		{
			throw new NotImplementedException();
		}

		void IDatabase.DeleteOrderProduct(int orderProduct)
		{
			throw new NotImplementedException();
		}

		void IDatabase.EditOrderProduct(OrderProduct orderProduct)
		{
			throw new NotImplementedException();
		}

		void IDatabase.UpdateOrderProduct(OrderProduct orderProduct)
		{
			throw new NotImplementedException();
		}
	}
}
