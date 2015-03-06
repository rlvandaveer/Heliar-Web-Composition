using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Heliar.ComponentModel.Composition.Web.Mvc
{
	/// <summary>
	///  Represents a composable part catalog for an MVC application.
	/// </summary>
	public class MvcApplicationCatalog : WebApplicationCatalog
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MvcApplicationCatalog"/> class.
		/// </summary>
		public MvcApplicationCatalog() : base() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="MvcApplicationCatalog"/> class.
		/// </summary>
		/// <param name="assemblies">The assemblies.</param>
		public MvcApplicationCatalog(IEnumerable<Assembly> assemblies) : base(assemblies) { }

		/// <summary>
		/// Defines the default conventions for part composition.
		/// </summary>
		/// <returns>ReflectionContext.</returns>
		protected override RegistrationBuilder DefineConventions(RegistrationBuilder conventions = null)
		{			
			conventions = base.DefineConventions(conventions);
			conventions.ForTypesDerivedFrom<IController>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.Export();
			conventions.ForTypesDerivedFrom<FilterAttribute>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.Export();
			conventions.ForTypesDerivedFrom<IModelBinder>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.Export();
			return conventions;
		}
	}
}
