using System;

namespace Heliar.ComponentModel.Composition.Web
{
	/// <summary>
	/// Responsible for cleaning up a composition container and its resources when they go
	/// out of scope, i.e. at the end of a web request.
	/// </summary>
	public static class CompositionScopeDisposer
	{
		/// <summary>
		/// Disposes of the composition scope.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		public static void DisposeCompositionScope(object sender, EventArgs e)
		{
			DisposeCompositionScope();
		}
	
		/// <summary>
		/// Disposes of the composition scope.
		/// </summary>
		public static void DisposeCompositionScope()
		{
			var scope = CompositionProvider.CurrentInitialisedScope;
			if (scope != null)
				scope.Dispose();
		}
	}
}
