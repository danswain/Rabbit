using System;
using EasyNetQ;

namespace FanoutPublisher
{
	public class RabbitLogger : IEasyNetQLogger
	{
		public void DebugWrite(string format, params object[] args)
		{
			
		}

		public void InfoWrite(string format, params object[] args)
		{
			
		}

		public void ErrorWrite(string format, params object[] args)
		{
			
		}

		public void ErrorWrite(Exception exception)
		{			
		}
	}
}