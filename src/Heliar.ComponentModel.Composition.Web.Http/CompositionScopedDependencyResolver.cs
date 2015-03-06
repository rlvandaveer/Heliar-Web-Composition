using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace Heliar.ComponentModel.Composition.Web.Http
{
	/// <summary>
	/// Resolves shared or web request scoped dependencies using a composition provider.
	/// </summary>
	public class CompositionScopedDependencyResolver : CompositionDependencyScope, IDependencyResolver
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CompositionScopedDependencyResolver"/> class.
		/// </summary>
		public CompositionScopedDependencyResolver() 
			: base(CompositionProvider.ApplicationScopeContainer) { }

		/// <summary>
		/// Starts a new resolution scope.
		/// </summary>
		/// <returns>The dependency scope.</returns>
		public IDependencyScope BeginScope()
		{
			return new CompositionDependencyScope(CompositionProvider.Current);
		}
	}
}