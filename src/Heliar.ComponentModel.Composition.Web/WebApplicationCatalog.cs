using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Heliar.ComponentModel.Composition.Web
{
	/// <summary>
	/// Represents a composable part catalog for a web application.
	/// </summary>
	public class WebApplicationCatalog : ComposablePartCatalog
	{
		/// <summary>
		/// The inner application catalog
		/// </summary>
		protected static ComposablePartCatalog applicationCatalog;

		/// <summary>
		/// Initializes a new instance of the <see cref="WebApplicationCatalog"/> class.
		/// </summary>
		/// <param name="assemblies">The assemblies.</param>
		/// <param name="reflectionContext">The reflection context.</param>
		protected WebApplicationCatalog(IEnumerable<Assembly> assemblies, ReflectionContext reflectionContext = null)
        {
			if (!IsInitialized)
			{
				if (reflectionContext == null)
					reflectionContext = DefineConventions();

				applicationCatalog = new AggregateCatalog(assemblies.Select(a => new AssemblyCatalog(a, reflectionContext)));
			}
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="WebApplicationCatalog" /> class. Will use default registrations for types in a "Parts" namespace.
		/// </summary>
        public WebApplicationCatalog()
            : this(new[] { GuessGlobalApplicationAssembly() }) { }

		/// <summary>
		/// Guesses the global application assembly.
		/// </summary>
		/// <returns>Assembly.</returns>
        internal protected static Assembly GuessGlobalApplicationAssembly()
        {
            return HttpContext.Current.ApplicationInstance.GetType().BaseType.Assembly;
        }

		/// <summary>
		/// Defines the default conventions.
		/// </summary>
		/// <returns>ReflectionContext.</returns>
		protected virtual RegistrationBuilder DefineConventions(RegistrationBuilder conventions = null)
		{
			if (conventions == null)
				conventions = new RegistrationBuilder();

			conventions.ForTypesMatching(IsAPart)
				.Export()
				.ExportInterfaces(t => t != typeof(IDisposable));

			conventions.ForTypesMatching(t => IsAPart(t) && t.GetCustomAttributes(typeof(ApplicationSharedAttribute), true).Any())
				.AddMetadata(CompositionProvider.ApplicationShared, true);

			return conventions;
		}

		/// <summary>
		/// Determines whether the specified type is a composable part.
		/// </summary>
		/// <param name="t">The t.</param>
		/// <returns><c>true</c> if the specified type is in the parts namespace; otherwise, <c>false</c>.</returns>
		private static bool IsAPart(Type t)
		{
			return !t.Name.EndsWith("Attribute") &&
									t.Namespace != null &&
									t.IsInNamespace("Parts");
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
				applicationCatalog.Dispose();

			base.Dispose(disposing);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the catalog.
		/// </summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			return applicationCatalog.GetEnumerator();
		}

		/// <summary>
		/// Gets the part definitions that are contained in the catalog.
		/// </summary>
		/// <value>The parts.</value>
		/// <returns>The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> contained in the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" />.</returns>
		public override IQueryable<ComposablePartDefinition> Parts
		{
			get
			{
				return applicationCatalog.Parts;
			}
		}

		/// <summary>
		/// Gets a list of export definitions that match the constraint defined by the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> object.
		/// </summary>
		/// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects to be returned.</param>
		/// <returns>A collection of <see cref="T:System.Tuple`2" /> containing the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects and their associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects for objects that match the constraint specified by <paramref name="definition" />.</returns>
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			return applicationCatalog.GetExports(definition);
		}

		/// <summary>
		/// Gets a value indicating whether this instance is initialized.
		/// </summary>
		/// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
		static bool IsInitialized
		{
			get
			{
				return applicationCatalog != null;
			}
		}
	}
}
