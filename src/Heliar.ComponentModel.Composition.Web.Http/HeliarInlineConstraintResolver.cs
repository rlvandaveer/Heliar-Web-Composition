// ***********************************************************************
// Assembly         : Heliar.ComponentModel.Composition.Web.Http
// Author           : R. L. Vandaveer
// Created          : 10-08-2015
//
// Last Modified By : R. L. Vandaveer
// Last Modified On : 10-09-2015
// ***********************************************************************
// <copyright file="HeliarInlineConstraintResolver.cs" company="">
//     Copyright ©2013 R. L. Vandaveer
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web.Http.Routing;

namespace Heliar.ComponentModel.Composition.Web.Http
{
	/// <summary>
	/// Resolves constraints and their dependencies for attributed routes.
	/// </summary>
	public class HeliarInlineConstraintResolver : IInlineConstraintResolver
	{
		/// <summary>
		/// Gets the constraint map.
		/// </summary>
		/// <value>The constraint map.</value>
		public IDictionary<string, Type> ConstraintMap { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HeliarInlineConstraintResolver" /> class.
		/// </summary>
		public HeliarInlineConstraintResolver()
		{
			this.ConstraintMap = new Dictionary<string, Type>();
		}

		/// <summary>
		/// Resolves the constraint and its dependencies.
		/// </summary>
		/// <param name="inlineConstraint">The inline constraint.</param>
		/// <returns>IHttpRouteConstraint.</returns>
		public IHttpRouteConstraint ResolveConstraint(string inlineConstraint)
		{
			Type type = this.ConstraintMap[inlineConstraint];
			return CompositionProvider.ApplicationScopeContainer.GetExportedValue<object>(AttributedModelServices.GetContractName(type)) as IHttpRouteConstraint;
		}
	}
}