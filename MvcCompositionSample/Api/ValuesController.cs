using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcCompositionSample.Parts;

namespace MvcCompositionSample.Api
{
	public class ValuesController : ApiController
	{
		private Foo foo;
		public ValuesController(Foo foo)
		{
			this.foo = foo;
		}
		// GET api/<controller>
		public IEnumerable<string> Get()
		{
			return this.foo.Foos;
		}

		// GET api/<controller>/5
		public string Get(int id)
		{
			return this.foo.Foos[id];
		}
	}
}