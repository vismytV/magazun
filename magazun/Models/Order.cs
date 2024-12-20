namespace magazun.Models
{
	public class Order //таблиця замовлення
	{
		public int OrderId { get; set; } // первинний ключ
		public DateTime OrderDate { get; set; } // Дата замовлення
		public int CustomerId { get; set; } // зовнішній ключ до Customers
		public decimal TotalAmount { get; set; } // Загальна сума замовлення

		// Зв'язок з таблицею Customer (Один до багатьох)
		public Customer Customer { get; set; }

		// Зв'язок з таблицею Product (Багато до багатьох)
		public ICollection<OrderProduct> OrderProducts { get; set; }
	}
}
