#What is Heliar-Web-Composition?

Heliar Web Composition is a set of helper libraries for doing web request scoped composition using MEF2 with MVC and WebAPI. It was written because I couldn't find anything that facilitated using MEF for both MVC and WebAPI.

##Heliar Web Composition consists of three projects:
1. Heliar.ComponentModel.Composition.Web
2. Heliar.ComponentModel.Composition.Web.Http
3. Heliar.ComponentModel.Composition.Web.Mvc

###Heliar.ComponentModel.Composition.Web
Provides the core composition functionality and container teardown.

####Components:
**CompositionProvider**: providers composition services to an application at both a shared application level, and at a web request scoped level

**WebApplicationCatalog**: acts as the base container for composed parts for a web application and provides helper methods for registering conventions.

**ApplicationSharedAttribute**: denotes items that should be shared across the application in a single application level composition container

**CompositionScopeDisposer**: responsible for teardown of a web request composition scope

###Heliar.ComponentModel.Composition.Web.Http
Applies the core composition and teardown functionality against WebAPI.

####Components:
**CompositionScopedDependencyResolver**: responsible for beginning a new dependency scope _per web request_.

**CompositionDependencyScope**: passes requests for web request scoped dependencies to the CompositionProvider. Also responsible for teardown of the composition scope.

**WebApiApplicationCatalog**: inherits from the WebApplicationCatalog and provides basic registration of WebAPI types i.e. controllers.

**WebApiResolverConfiguration**: wires the CompositionScopedDependencyResolver as the WebAPI's dependency resolver.

###Heliar.ComponentModel.Composition.Web.Mvc
Applies the core composition functionality against MVC and assists in tearing down of dependency scope at the end of a request.

####Components:
**CompositionScopedDependencyResolver**: passes requests for web request scoped dependencies to the CompositionProvider.

**CompositionScopedFilterAttributeFilterProvider**: uses the CompositionProvider to compose filter dependencies.

**CompositionScopedModelBinderProvider**: uses the CompositionProvider to compose model binder dependencies.

**ModelBinderExportAttribute**: assists exporting model binders for composition

**MvcApplicationCatalog**: inherits from the WebApplicationCatalog and provides basic registration of MVC types i.e. controllers, action filters, model binders.

**MvcResolverConfiguration**: wires the CompositionScopedDependencyResolver as MVC's dependency resolver.

**RequestScopedCompositionModule**: HttpModule that uses the CompositionScopeDisposer to tear down the current composition scope.

##How do I use it?
Depending on whether you are using MVC and/or WebAPI you will need to reference _Heliar.ComponentModel.Composition.Web_, _Heliar.ComponentModel.Composition.Web.Mvc_, and/or _Heliar.ComponentModel.Composition.Web.Http_ and wire up composition similar to the included MvcCompositionSample.