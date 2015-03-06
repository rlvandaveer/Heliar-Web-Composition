using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCompositionSample.Parts
{
	public class Foo : IDisposable
	{
		public IList<string> Foos;

		public Foo()
		{
			this.Foos = new List<string>();
			this.Foos.Add("Single Responsibility Principle");
			this.Foos.Add("Open/Closed Principle");
			this.Foos.Add("Liskov Substitution Principle");
			this.Foos.Add("Interface Segregation Principle");
			this.Foos.Add("Dependency Inversion Principle");
		}

		public void Dispose()
		{
			this.Foos = null;
		}
	}
}