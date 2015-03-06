
namespace Heliar.ComponentModel.Composition.Web
{
	/// <summary>
	/// Represents a composition configuration. Implement this class to provide custom
	/// wire up of composition providers and dependency resolvers.
	/// </summary>
	public interface IResolverConfiguration
	{
		/// <summary>
		/// Configures the composition.
		/// </summary>
		void ConfigureResolver();
	}
}