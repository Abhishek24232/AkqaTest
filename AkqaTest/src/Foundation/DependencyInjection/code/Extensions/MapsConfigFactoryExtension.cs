
using System;
using System.Reflection;
using AkqaTest.Foundation.DependencyInjection.Methods;
using System.Web.Mvc;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using AkqaTest.Foundation.DependencyInjection.Attributes;

namespace AkqaTest.Foundation.DependencyInjection.Extensions
{
    public static class MapsConfigFactoryExtension
    {
       
        public static void AddMvcControllers(this IServiceCollection serviceCollection, params string[] assemblyFilters)
        {
            var assemblies = GetAssemblies.GetByFilter(assemblyFilters);

            AddMvcControllers(serviceCollection, assemblies);
        }

        public static void AddMvcControllers(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            var controllers = GetTypes.GetTypesImplementing<IController>(assemblies)
                .Where(controller => controller.Name.EndsWith("Controller", StringComparison.Ordinal));

            foreach (var controller in controllers)
            {
                serviceCollection.AddTransient(controller);
            }
        }
        public static void AddServiceAttribute(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddServiceAttribute(Assembly.GetCallingAssembly());
        }

        public static void AddServiceAttribute(this IServiceCollection serviceCollection, params string[] assemblyFilters)
        {
            var assemblies = GetAssemblies.GetByFilter(assemblyFilters);
            serviceCollection.AddServiceAttribute(assemblies);
        }

        public static void AddServiceAttribute(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            var typesWithAttributes = assemblies
                .Where(assembly => !assembly.IsDynamic)
                .SelectMany(GetExportedTypes)
                .Where(type => !type.IsAbstract && !type.IsGenericTypeDefinition)
                .Select(type => new { type.GetCustomAttribute<LifetimeAttribute>()?.Lifetime, ServiceType = type, ImplementationType = type.GetCustomAttribute<LifetimeAttribute>()?.ServiceType })
                .Where(t => t.Lifetime != null);

            foreach (var type in typesWithAttributes)
            {
                if (type.ImplementationType == null)
                    serviceCollection.Add(type.ServiceType, type.Lifetime.Value);
                else
                    serviceCollection.Add(type.ImplementationType, type.ServiceType, type.Lifetime.Value);
            }
        }
        public static void Add(this IServiceCollection serviceCollection, Lifetime lifetime, params Type[] types)
        {
            foreach (var type in types)
            {
                serviceCollection.Add(type, lifetime);
            }
        }

        public static void Add<T>(this IServiceCollection serviceCollection, Lifetime lifetime)
        {
            serviceCollection.Add(typeof(T), lifetime);
        }

        public static void Add(this IServiceCollection serviceCollection, Type type, Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton:
                    serviceCollection.AddSingleton(type);
                    break;
                case Lifetime.Transient:
                    serviceCollection.AddTransient(type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        public static void Add(this IServiceCollection serviceCollection, Type serviceType, Type implementationType, Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton:
                    serviceCollection.AddSingleton(serviceType, implementationType);
                    break;
                case Lifetime.Transient:
                    serviceCollection.AddTransient(serviceType, implementationType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }
        private static IEnumerable<Type> GetExportedTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetExportedTypes();
            }
            catch (NotSupportedException)
            {
                // A type load exception would typically happen on an Anonymously Hosted DynamicMethods
                // Assembly and it would be safe to skip this exception.
                return Type.EmptyTypes;
            }
            catch (FileLoadException)
            {
                // The assembly points to a not found assembly - ignore and continue
                return Type.EmptyTypes;
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Return the types that could be loaded. Types can contain null values.
                return ex.Types.Where(type => type != null);
            }
            catch (Exception ex)
            {
                // Throw a more descriptive message containing the name of the assembly.
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unable to load types from assembly {0}. {1}", assembly.FullName, ex.Message), ex);
            }
        }

    }
}