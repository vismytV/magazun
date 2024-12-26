namespace magazun.Models
{
	public class Product //таблиця товари
	{
		public int ProductId { get; set; } // первинний ключ
		public string Name { get; set; } // Назва товару
		public double Price { get; set; } // Ціна
		public string? Description { get; set; } // Опис товару

		// Зв'язок з таблицею Orders (Багато до багатьох)
		public ICollection<OrderProduct> OrderProducts { get; set; }
	}
}
