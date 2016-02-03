using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInstanceFactory.Sample.Interfaces;

namespace CommonInstanceFactory.Sample.Data
{
	public class WesternGreetingRepository : IGreetingRepository
	{
		public string GetGreeting()
		{
			return "Howdy";
		}
	}
}
