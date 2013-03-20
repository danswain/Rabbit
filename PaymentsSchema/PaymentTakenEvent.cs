namespace PaymentsSchema
{
	public class PaymentTakenEvent
	{
		public int Id { get; set; }
		public int Amount { get; set; }
		public string Message { get; set; }
	}
}