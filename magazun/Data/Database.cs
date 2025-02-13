﻿using magazun.Models;
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
			_context.Customers.Add(customer); // Добавление клиента
			_context.SaveChanges(); // Сохранение изменений в базе данных
		}
		/*void IDatabase.AddProduct(Product product)
		{
			_context.Products.Add(product);
			_context.SaveChanges(); // Сохранение изменений в базе данных
		}*/

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
			var existingCustomer = _context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
			if (existingCustomer == null)
			{
				throw new Exception("Клиент не найден");
			}

			// Проверка на уникальность Email
			if (_context.Customers.Any(c => c.Email == customer.Email && c.CustomerId != customer.CustomerId))
			{
				throw new Exception("Email вже використовується іншим клієнтом");
			}

			existingCustomer.FirstName = customer.FirstName;
			existingCustomer.LastName = customer.LastName;
			existingCustomer.Email = customer.Email;

			_context.SaveChanges();
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
			_context.Products.Add(product); 
			_context.SaveChanges(); // Сохранение изменений в базе данных
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
			// знаходимо користувача
			var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
			

			// Добавляем сам Order
			_context.Orders.Add(order);

			// Добавляем связанные записи в таблицу OrderProduct
			if (order.OrderProducts != null && order.OrderProducts.Any())
			{
				foreach (var orderProduct in order.OrderProducts)
				{
					// Знаходимо вказанный Product
					var product = _context.Products.FirstOrDefault(p => p.ProductId == orderProduct.ProductId);
					if (product == null)
						throw new InvalidOperationException($"Product with ID {orderProduct.ProductId} does not exist.");

					// Устанавливаем связь между Order и Product
					orderProduct.Order = order; // Указываем связь с текущим Order
					_context.OrderProducts.Add(orderProduct);
				}
			}

			// Сохраняем изменения в базе данных
			_context.SaveChanges();
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

		void IDatabase.AddLogin(Login login)
		{
			_context.Logins.Add(login); // Добавление логина
			_context.SaveChanges(); // Сохранение изменений в базе данных
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
			var existingLogin = _context.Logins.FirstOrDefault(l => l.Id == login.Id);

			if (existingLogin == null)
			{
				throw new Exception("Логін не найдено");
			}

			existingLogin.Password=login.Password;
			existingLogin.Role=login.Role;
			
			_context.SaveChanges();
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
