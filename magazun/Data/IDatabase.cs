using magazun.Models;

namespace magazun.Data
{
	public interface IDatabase
	{
		// Методы для работы с Customer//таблиця клієнти
		List<Customer> GetCustomer();
		void AddCustomer(Customer customer);
		void DeleteCustomer(int customerId);
		void EditCustomer(Customer customer);
		void UpdateCustomer(Customer customer);

		// Методы для работы с Product//таблиця продукти
		List<Product> GetProduct();
		void AddProduct(Product product);
		void DeleteProduct(int product);
		void EditProduct(Product product);
		void UpdateProduct(Product product);

		// Методы для работы с Order //таблиця замовлення
		//List<Order> GetOrder();
		IQueryable<Order> GetOrder();
		void AddOrder(Order order);
		void DeleteOrder(int order);
		void EditOrder(Order order);
		void UpdateOrder(Order order);

		//Login
		List<Login> GetLogin();
		void AddLogin(Login login);
		void DeleteLogin(int login);
		void EditLogin(Login login);
		void UpdateLogin(Login login);

		//OrderProducts
		List<OrderProduct> GetOrderProduct();
		void AddOrderProduct(OrderProduct orderProduct);
		void DeleteOrderProduct(int orderProduct);
		void EditOrderProduct(OrderProduct orderProduct);
		void UpdateOrderProduct(OrderProduct orderProduct);

	}
}
