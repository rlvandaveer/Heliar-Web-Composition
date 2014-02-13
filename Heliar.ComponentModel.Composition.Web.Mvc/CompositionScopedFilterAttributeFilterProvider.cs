using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;

namespace Heliar.ComponentModel.Composition.Web.Mvc
{
	/// <summary>
	/// Uses a scoped composition provider to resolve filter attributes.
	/// </summary>
    class CompositionScopedFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="CompositionScopedFilterAttributeFilterProvider"/> class.
		/// </summary>
        public CompositionScopedFilterAttributeFilterProvider()
            : base(cacheAttributeInstances: false) { }

		/// <summary>
		/// Gets a collection of custom action attributes.
		/// </summary>
		/// <param name="controllerContext">The controller context.</param>
		/// <param name="actionDescriptor">The action descriptor.</param>
		/// <returns>A collection of custom action attributes.</returns>
        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetActionAttributes(controllerContext, actionDescriptor).ToArray();
            ComposeAttributes(attributes);
            return attributes;
        }

		/// <summary>
		/// Gets a collection of controller attributes.
		/// </summary>
		/// <param name="controllerContext">The controller context.</param>
		/// <param name="actionDescriptor">The action descriptor.</param>
		/// <returns>A collection of controller attributes.</returns>
        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor).ToArray();
            ComposeAttributes(attributes);
            return attributes;
        }

		/// <summary>
		/// Composes the attributes.
		/// </summary>
		/// <param name="attributes">The attributes.</param>
        void ComposeAttributes(FilterAttribute[] attributes)
        {
            CompositionProvider.Current.ComposeParts(attributes);
        }
    }
}
