using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using CommonInstanceFactory.Sample.Client.GreetingService;

namespace CommonInstanceFactory.Sample.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			string endpoint;
			Console.WriteLine("Select host: Console {C}, Web {W}");
			ConsoleKey hostKey = Console.ReadKey(true).Key;
			Console.WriteLine(hostKey.ToString().ToUpper());
			if (hostKey == ConsoleKey.C)
			{
				Console.WriteLine("Make sure console host is running then press Enter");
				Console.ReadLine();
				endpoint = "console";
			}
			else
			{
				Console.WriteLine("Select service: Ninject {N}, SimpleInjector {S}");
				ConsoleKey containerKey = Console.ReadKey(true).Key;
				Console.WriteLine(containerKey.ToString().ToUpper());
				endpoint = GetEndpoint(containerKey);
			}
			if (endpoint == null) return;

			Console.WriteLine("Select protocol: Soap {S}, Rest {R}");
			ConsoleKey protocolKey = Console.ReadKey(true).Key;
			Console.WriteLine(protocolKey.ToString().ToUpper());
			
			if (protocolKey == ConsoleKey.S)
			{
				UseSoap(endpoint);
			}
			else if (protocolKey == ConsoleKey.R)
			{
				string restBaseAddress = ConfigurationManager.AppSettings[endpoint + "-rest"];
				UseRest(restBaseAddress);
			}
			else
			{
				Console.WriteLine("Invalid selection");
			}
		}

		private static void UseRest(string restBaseAddress)
		{
			var webClient = new WebClient();
			webClient.BaseAddress = restBaseAddress;
			string response = webClient.DownloadString("Greet?name=Tony");
			Console.WriteLine(response);
		}

		private static void UseSoap(string endpoint)
		{
			using (var client = new GreetingServiceClient(endpoint))
			{
				string response = client.Greet("Tony");
				Console.WriteLine(response);
			}
		}

		private static string GetEndpoint(ConsoleKey containerKey)
		{
			string endpoint;
			switch (containerKey)
			{
				case ConsoleKey.N:
					endpoint = "ninject-web";
					break;
				case ConsoleKey.S:
					endpoint = "simpleinjector-web";
					break;
				default:
					Console.WriteLine("Invalid container selection");
					return null;
			}
			return endpoint;
		}
	}
}
