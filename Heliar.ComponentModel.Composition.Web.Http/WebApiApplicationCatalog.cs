using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Heliar.ComponentModel.Composition.Web.Http
{
	/// <summary>
	/// Represents a composable part catalog for a WebAPI application. This class cannot be inherited.
	/// </summary>
	public sealed class WebApiApplicationCatalog : WebApplicationCatalog
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WebApiApplicationCatalog"/> class.
		/// </summary>
		public WebApiApplicationCatalog() : base() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="WebApiApplicationCatalog"/> class.
		/// </summary>
		/// <param name="assemblies">The assemblies.</param>
		public WebApiApplicationCatalog(IEnumerable<Assembly> assemblies) : base(assemblies) { }

		/// <summary>
		/// Defines the default WebAPI conventions for composing parts.
		/// </summary>
		/// <returns>ReflectionContext.</returns>
		protected override RegistrationBuilder DefineConventions(RegistrationBuilder conventions = null)
		{
			conventions = base.DefineConventions(conventions);
			conventions.ForTypesDerivedFrom<IHttpController>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.Export();

			return conventions;
		}
	}
}