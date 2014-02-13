using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using Heliar.ComponentModel.Composition.Web;
using Heliar.ComponentModel.Composition.Web.Http;
using Heliar.ComponentModel.Composition.Web.Mvc;

namespace MvcCompositionSample
{
	public static class DependencyConfig
	{
		internal static CompositionProvider compositionProvider;

		public static void RegisterDependencies()
		{
			var configs = new List<IResolverConfiguration>();
			configs.Add(new MvcResolverConfiguration());
			configs.Add(new WebApiResolverConfiguration());
			var sampleAppCatalog = new MvcCompositionSampleCatalog();
			var catalog = new AggregateCatalog(sampleAppCatalog);
			// Configure any other dependencies you need
			compositionProvider = new CompositionProvider(catalog, configs.ToArray());
		}

	}
}