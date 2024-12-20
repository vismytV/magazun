namespace magazun.Models
{
	public class OrderProduct//проміжна таблиця для реалізації зв'язку між покупцум та тотоваром
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }

		public Order Order { get; set; }
		public Product Product { get; set; }
	}
}
