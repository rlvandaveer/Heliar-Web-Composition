
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Web;

namespace Heliar.ComponentModel.Composition.Web
{
	/// <summary>
	/// Provides composition services to an application.
	/// </summary>
	public class CompositionProvider
	{
		/// <summary>
		/// Metadata to assist composing shared components.
		/// </summary>
		public const string ApplicationShared = "CompositionProvider.ApplicationShared";

		/// <summary>
		/// The application scope container
		/// </summary>
		protected static CompositionContainer applicationScopeContainer;
		/// <summary>
		/// The request scope catalog
		/// </summary>
		protected static ComposablePartCatalog requestScopeCatalog;

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositionProvider"/> class.
		/// </summary>
		/// <param name="initialCatalog">The initial catalog.</param>
		/// <param name="resolverConfigs">The composition configs.</param>
		public CompositionProvider(ComposablePartCatalog initialCatalog, IResolverConfiguration[] resolverConfigs = null)
		{
			if (initialCatalog == null)
				initialCatalog = new WebApplicationCatalog();

			var globals = initialCatalog.Filter(cpd => cpd.ContainsPartMetadata(ApplicationShared, true));
			applicationScopeContainer = new CompositionContainer(globals, CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);

			requestScopeCatalog = globals.Complement;

			if (resolverConfigs != null && resolverConfigs.Length > 0)
			{
				foreach (var config in resolverConfigs)
				{
					config.ConfigureResolver();
				}
			}
		}

		/// <summary>
		/// Gets the composition container for the current scope.
		/// </summary>
		/// <value>The current.</value>
		public static CompositionContainer Current
		{
			get
			{
				return CurrentInitialisedScope ?? (CurrentInitialisedScope = new CompositionContainer(requestScopeCatalog,
																									  CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe, 
																									  applicationScopeContainer));
			}
		}

		/// <summary>
		/// Gets the initialized composition container for the current scope.
		/// </summary>
		/// <value>The current initialised scope.</value>
		public static CompositionContainer CurrentInitialisedScope
		{
			get
			{
				return (CompositionContainer)HttpContext.Current.Items[typeof(CompositionProvider)]; 
			}
			private set
			{
				HttpContext.Current.Items[typeof(CompositionProvider)] = value; 
			}
		}

		/// <summary>
		/// Gets the application scope container.
		/// </summary>
		/// <value>The application scope container.</value>
		public static CompositionContainer ApplicationScopeContainer
		{
			get
			{
				return applicationScopeContainer;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is initialized.
		/// </summary>
		/// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
		static bool IsInitialized
		{
			get
			{
				return applicationScopeContainer != null;
			}
		}
	}
}
