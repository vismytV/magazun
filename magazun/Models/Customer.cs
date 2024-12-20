namespace magazun.Models
{
	public class Customer//таблиця клієнти
	{
		public int CustomerId { get; set; } // первинний ключ
		public string FirstName { get; set; } // Ім'я
		public string LastName { get; set; } // Прізвище
		public string Email { get; set; } // Email

		// Зв'язок з таблицею Orders (Один до багатьох)
		public ICollection<Order> Orders { get; set; }
	}
}
