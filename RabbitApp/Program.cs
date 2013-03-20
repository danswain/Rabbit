using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using PaymentsSchema;

namespace RabbitApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var bus = RabbitHutch.CreateBus("host=localhost");
			var paymentTaken = new PaymentTakenEvent
				                   {
					                   Id = 1,
									   Amount = 123,
									   Message = "For Britney"
				                   };
			var next = new Random(2);
			bool keepRunning = true;
			while (keepRunning)
			{
				
				var id = next.Next();
				using (var publishChannel = bus.OpenPublishChannel())
				{

					publishChannel.Publish(new PaymentTakenEvent
						                       {
							                       Amount = 123,
												   Id = id,
												   Message = "Bonjovi"
						                       });
				}

				Thread.Sleep(500);
			}



		}
	}
}
