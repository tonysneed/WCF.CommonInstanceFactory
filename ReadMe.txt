Common Instance Factory ReadMe

The purpose of the Common Instance Factory to to decouple an application
from a direct dependency on a specific Dependency Injection container.
This frees developers to switch containers for considerations such as performance.
This should be adequate for applications that use dependency injection.
For unit testing, however, it's best to select an DI container based on
the features it offers and to avoid the "lowest common denominator" effect
of abstracting away the DI container behind the Common Instance Factory.

Common Instance Factory is similar to Common Service Locator written by
Microsoft Patterns and Practices group, but it uses the abstract factory
pattern instead of the service locator (anti-)pattern.  It also sticks with
generic methods for better type safety and includes a ReleaseInstance method.

Creating an adapter for Common Instance Factory is as simple as inheriting
from CommonInstanceFactoryBase and overriding a few absract methods.

Creating an adapter for the WCF extensions is also quite easy. Simply
create a class that inherits from ServiceHost and accepts a DI container
as a constructor argument.  Then create the instance factory and pass it
to the ctor of the InjectedServiceBehavior that is added to the ServiceHost
Description.

The samples include both console and web hosts and a client to consume them.