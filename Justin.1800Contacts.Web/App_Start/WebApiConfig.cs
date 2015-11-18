using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Justin._1800Contacts.Api.Interface;
using Justin._1800Contacts.Api.Logic;
using Justin._1800Contacts.Api.Sorting;
using Microsoft.Practices.Unity;

namespace Justin._1800Contacts.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Ideally these dependencies should be initialized in another module
			var container = new UnityContainer();
			container.RegisterType<IClassLogic, ClassLogic>(new HierarchicalLifetimeManager());
			container.RegisterType<ISorter, TopologicalSorter>(new HierarchicalLifetimeManager());
			config.DependencyResolver = new UnityResolver(container);

			// Making the default API content type JSON and removing XML because XML sucks
			JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;
			jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
			config.Formatters.Remove(config.Formatters.XmlFormatter);

			// Using route attributes on the induvidual API controllers and not defining them here
			config.MapHttpAttributeRoutes();
        }
    }
}
