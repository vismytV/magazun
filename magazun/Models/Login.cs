namespace magazun.Models
{
	public class Login
	{
		public int Id { get; set; }

		public string UserLogin { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }

		public int idCustomer{ get; set; }
		public Customer customer { get; set; }
	}
}
