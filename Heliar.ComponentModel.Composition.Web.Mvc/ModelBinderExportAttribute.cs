using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Heliar.ComponentModel.Composition.Web.Mvc
{
	/// <summary>
	/// ModelBinderExportAttribute.
	/// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class ModelBinderExportAttribute : ExportAttribute
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="ModelBinderExportAttribute"/> class.
		/// </summary>
		/// <param name="modelType">Type of the model.</param>
        public ModelBinderExportAttribute(Type modelType)
            : base(CompositionScopedModelBinderProvider.GetModelBinderContractName(modelType), typeof(IModelBinder)) { }
    }
}
