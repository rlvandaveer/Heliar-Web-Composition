using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Heliar.ComponentModel.Composition.Web.Http
{
	/// <summary>
	/// Helper class for configuring composition and wiring up the WebAPI dependency resolver.
	/// </summary>
	public class WebApiResolverConfiguration : IResolverConfiguration
	{
		/// <summary>
		/// Configures the dependency resolver.
		/// </summary>
		public void ConfigureResolver()
		{
			var resolver = new CompositionScopedDependencyResolver();
			GlobalConfiguration.Configuration.DependencyResolver = resolver;
		}
	}
}