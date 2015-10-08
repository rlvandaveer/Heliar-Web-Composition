using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace Heliar.ComponentModel.Composition.Web.Http
{
	public class HeliarInlineConstraintResolver : IInlineConstraintResolver
	{
		public IDictionary<string, Type> ConstraintMap { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HeliarInlineConstraintResolver"/> class.
		/// </summary>
		public HeliarInlineConstraintResolver()
		{
			this.ConstraintMap = new Dictionary<string, Type>();
		}

		public IHttpRouteConstraint ResolveConstraint(string inlineConstraint)
		{
			Type type = this.ConstraintMap[inlineConstraint];
			return CompositionProvider.ApplicationScopeContainer.GetExportedValueOrDefault<IHttpRouteConstraint>(AttributedModelServices.GetContractName(type));
		}
	}
}
