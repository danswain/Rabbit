using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using PaymentsSchema;

namespace FanoutSubscribe
{
	class Program
	{
		static void Main(string[] args)
		{
			var logger = new FanoutPublisher.RabbitLogger();
			var advancedBus = RabbitHutch.CreateBus("host=localhost", reg => reg.Register<IEasyNetQLogger>(_ => logger)).Advanced;
			var randQueue = new Random();
			var queue = Queue.DeclareTransient("subscriber_" + randQueue.Next(100));			
			var exchange = Exchange.DeclareFanout("dan.fanout.name");
			queue.BindTo(exchange, "routing-key");

			advancedBus.Subscribe<PaymentTakenEvent>(queue, (msg, messageReceivedInfo) =>
			                                                Task.Factory.StartNew(() =>
				                                                                      {
					                                                                      Console.WriteLine("Got Message: {0}",msg.Body.Message);			
				                                                                      }));


		}
	}
}
