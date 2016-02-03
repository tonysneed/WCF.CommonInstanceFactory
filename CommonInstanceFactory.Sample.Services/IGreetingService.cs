using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace CommonInstanceFactory.Sample.Services
{
	[ServiceContract]
	public interface IGreetingService
	{
		[WebGet]
		[OperationContract]
		string Greet(string name);
	}
}
