using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace Heliar.ComponentModel.Composition.Web.Mvc
{
	/// <summary>
	/// Uses a scoped composition provider to resolve model binders.
	/// </summary>
    class CompositionScopedModelBinderProvider : IModelBinderProvider
    {
		/// <summary>
		/// The model binder contract name suffix
		/// </summary>
        const string ModelBinderContractNameSuffix = "++ModelBinder";

		/// <summary>
		/// Gets the name of the model binder contract.
		/// </summary>
		/// <param name="modelType">Type of the model.</param>
		/// <returns>System.String.</returns>
        public static string GetModelBinderContractName(Type modelType)
        {
            return AttributedModelServices.GetContractName(modelType) + ModelBinderContractNameSuffix;
        }

		/// <summary>
		/// Returns the model binder for the specified type.
		/// </summary>
		/// <param name="modelType">The type of the model.</param>
		/// <returns>The model binder for the specified type.</returns>
        public IModelBinder GetBinder(Type modelType)
        {
            return CompositionProvider.Current.GetExportedValueOrDefault<IModelBinder>(GetModelBinderContractName(modelType));
        }
    }
}
