using System;
using System.Collections.Generic;
using System.Linq;
using CommonInstanceFactory.Sample.Interfaces;

namespace CommonInstanceFactory.Sample.Services
{
	public class GreetingService : IGreetingService
	{
		private readonly IGreetingRepository _greetingRepository;

		public GreetingService(IGreetingRepository greetingRepository)
		{
			_greetingRepository = greetingRepository;
		}

		public string Greet(string name)
		{
			string greeting = _greetingRepository.GetGreeting();
			return string.Format("{0} {1}", greeting, name);
		}
	}
}
