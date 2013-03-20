using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using PaymentsSchema;
using Queue = EasyNetQ.Topology.Queue;


namespace FanoutPublisher
{
	class Program
	{
		static void Main(string[] args)
		{
			var logger = new RabbitLogger();
			var advancedBus = RabbitHutch.CreateBus("host=localhost",reg=>reg.Register<IEasyNetQLogger>(_=>logger)).Advanced;
	
			var queue = Queue.DeclareTransient("publishQueue");
			var exchange = Exchange.DeclareFanout("dan.fanout.name");            
            queue.BindTo(exchange, "ignored");
			
			var random = new Random(2);
			while (true)
			{
				using (var channel = advancedBus.OpenPublishChannel())
				{
					var paymentTakenEvent = new PaymentTakenEvent
						                        {
							                        Id = random.Next(),
							                        Amount = random.Next(100),
							                        Message = "tea --> " + Guid.NewGuid().ToString()
						                        };

					channel.Publish(exchange, "ignored", new Message<PaymentTakenEvent>(paymentTakenEvent));
					Console.WriteLine("Published " + paymentTakenEvent.Message);
				}
				Thread.Sleep(1000);
			}
		}
	}
}
