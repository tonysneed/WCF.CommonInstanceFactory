using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using CommonInstanceFactory.Extensions.Wcf.Ninject;
using CommonInstanceFactory.Extensions.Wcf.SimpleInjector;
using CommonInstanceFactory.Extensions.Wcf.Unity;
using CommonInstanceFactory.Sample.Services;
using Ninject;
using NinjectModules = CommonInstanceFactory.Sample.DependencyResolution.Ninject.Modules;
using SimpleInjector;
using CommonInstanceFactory.Sample.DependencyResolution.SimpleInjector;
using CommonInstanceFactory.Sample.DependencyResolution.Unity;
using Microsoft.Practices.Unity;
using SimpleInjectorModules = CommonInstanceFactory.Sample.DependencyResolution.SimpleInjector.Modules;
using UnityModules = CommonInstanceFactory.Sample.DependencyResolution.Unity.Modules;

namespace CommonInstanceFactory.Sample.Hosting.Console
{
    class Program
    {
        enum ContainerType
        {
            Ninject, SimpleInjector, Unity
        }

        static void Main(string[] args)
        {
            // Change this to switch between containers
            ContainerType containerType;
            System.Console.WriteLine("Select container: Ninject {N}, SimpleInjector {S}, Unity {U}");
            ConsoleKey containerKey = System.Console.ReadKey(true).Key;
            System.Console.WriteLine(containerKey.ToString().ToUpper());
            switch (containerKey)
            {
                case ConsoleKey.N:
                    containerType = ContainerType.Ninject;
                    break;
                case ConsoleKey.S:
                    containerType = ContainerType.SimpleInjector;
                    break;
                case ConsoleKey.U:
                    containerType = ContainerType.Unity;
                    break;
                default:
                    System.Console.WriteLine("Invalid selection");
                    return;
            }

            System.Console.Title = "Greeting Service using " + containerType;

            // Create service host
            ServiceHost serviceHost;
            var serviceBaseAddress = new Uri("http://localhost:8000/GreetingService");
            switch (containerType)
            {
                case ContainerType.Ninject:
                    serviceHost = new NinjectServiceHost<GreetingService>
                        (CreateNinjectContainer(), serviceBaseAddress);
                    break;
                case ContainerType.SimpleInjector:
                    serviceHost = new SimpleInjectorServiceHost<GreetingService>
                        (CreateSimpleInjectorContainer(), serviceBaseAddress);
                    break;
                case ContainerType.Unity:
                    serviceHost = new UnityServiceHost<GreetingService>
                        (CreateUnityContainer(), serviceBaseAddress);
                    break;
                default:
                    System.Console.WriteLine("Unsupported container: " + containerType);
                    return;
            }

            // Start service host
            using (serviceHost)
            {
                serviceHost.Open();
                System.Console.WriteLine("Endpoints:");
                foreach (var endpoint in serviceHost.Description.Endpoints)
                {
                    System.Console.WriteLine("{0}: {1}",
                        endpoint.Binding.Name, endpoint.Address);
                }
                System.Console.WriteLine();
                System.Console.WriteLine("Greeting service is using {0}.\nPress Enter to exit.",
                    containerType);
                System.Console.ReadLine();
            }
        }

        private static IKernel CreateNinjectContainer()
        {
            IKernel container = new StandardKernel();
            container.Load<NinjectModules.GreetingModule>();
            return container;
        }

        private static Container CreateSimpleInjectorContainer()
        {
            var container = new Container();
            container.Load<SimpleInjectorModules.GreetingModule>();
            return container;
        }

        private static UnityContainer CreateUnityContainer()
        {
            var container = new UnityContainer();
            container.Load<UnityModules.GreetingModule>();
            return container;
        }
    }
}
