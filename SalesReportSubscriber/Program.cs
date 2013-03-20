using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using PaymentsSchema;

namespace SalesReportSubscriber
{
	class Program
	{
		static void Main(string[] args)
		{
			var bus = RabbitHutch.CreateBus("host=localhost");

			bus.Subscribe<PaymentTakenEvent>("my_subscription_id", msg =>
				                                                       {
					                                                       Console.WriteLine("{0} Purchased with Id {1}",msg.Message,msg.Id);
					                                                       //Console.WriteLine("Hi Dan");
				                                                       });
		}
	}
}
