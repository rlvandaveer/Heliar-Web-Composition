using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Heliar.ComponentModel.Composition.Web.Http
{
	public class CompositionScopedFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
	{
		
		public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
		{
			var filters = base.GetFilters(configuration, actionDescriptor).ToArray();
			this.ComposeFilters(filters);
			return filters;
		}

		void ComposeFilters(FilterInfo[] filters)
		{
			CompositionProvider.Current.ComposeParts(filters);
		}
	}
}
