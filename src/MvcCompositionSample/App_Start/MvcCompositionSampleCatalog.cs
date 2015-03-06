using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Heliar.ComponentModel.Composition.Web;

namespace MvcCompositionSample
{
	/// <summary>
	/// Sample catalog that combines MVC and WebAPI conventions.
	/// </summary>
	public class MvcCompositionSampleCatalog : WebApplicationCatalog
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MvcCompositionSampleCatalog"/> class.
		/// </summary>
		public MvcCompositionSampleCatalog() : base() { }

		/// <summary>
		/// Defines registration conventions for the sample application.
		/// </summary>
		/// <param name="conventions">The conventions.</param>
		/// <returns>ReflectionContext.</returns>
		protected override RegistrationBuilder DefineConventions(RegistrationBuilder conventions = null)
		{
			conventions = base.DefineConventions(conventions);

			DefineMvcConventions(conventions);
			DefineWebApiConventions(conventions);

			conventions.ForTypesMatching(t => t.GetCustomAttributes(typeof(ApplicationSharedAttribute), true).Any())
				.AddMetadata(CompositionProvider.ApplicationShared, true);

			return conventions;
		}

		/// <summary>
		/// Defines the WebAPI conventions.
		/// </summary>
		/// <param name="conventions">The conventions.</param>
		/// <returns>RegistrationBuilder.</returns>
		private RegistrationBuilder DefineWebApiConventions(RegistrationBuilder conventions)
		{
			conventions.ForTypesDerivedFrom<IHttpController>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.Export();
			return conventions;
		}

		/// <summary>
		/// Defines the MVC conventions.
		/// </summary>
		/// <param name="conventions">The conventions.</param>
		/// <returns>RegistrationBuilder.</returns>
		private RegistrationBuilder DefineMvcConventions(RegistrationBuilder conventions)
		{
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