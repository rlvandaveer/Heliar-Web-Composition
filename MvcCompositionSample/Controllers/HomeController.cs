using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCompositionSample.Parts;

namespace MvcCompositionSample.Controllers
{
	public class HomeController : Controller
	{
		Foo foo;

		public HomeController(Foo foo)
		{
			this.foo = foo;
		}

		public ActionResult Index()
		{
			ViewBag.Foo = this.foo;
			return View();
		}
	}
}