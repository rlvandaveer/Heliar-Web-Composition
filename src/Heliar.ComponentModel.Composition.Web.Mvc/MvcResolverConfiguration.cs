
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Heliar.ComponentModel.Composition.Web.Mvc
{
	/// <summary>
	/// Configures composition related to MVC applications and sets the dependency resolver.
	/// </summary>
	public class MvcResolverConfiguration : IResolverConfiguration
	{
		/// <summary>
		/// Configures the composition.
		/// </summary>
		public void ConfigureResolver()
		{
			this.SetResolver();
			FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().SingleOrDefault());
			FilterProviders.Providers.Add(new CompositionScopedFilterAttributeFilterProvider());
			ModelBinderProviders.BinderProviders.Add(new CompositionScopedModelBinderProvider());
		}

		/// <summary>
		/// Sets the resolver.
		/// </summary>
		private void SetResolver()
		{
			var resolver = new CompositionScopedDependencyResolver();
			DependencyResolver.SetResolver(resolver);
		}
	}
}