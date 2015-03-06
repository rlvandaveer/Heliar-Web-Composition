using System;

namespace Heliar.ComponentModel.Composition.Web
{
	/// <summary>
	/// This attribute designates that a part's lifetime should be scoped to the application.
	/// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ApplicationSharedAttribute : Attribute
    {
    }
}
