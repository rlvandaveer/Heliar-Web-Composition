namespace System
{
    static class TypeExtensions
    {
        public static bool IsInNamespace(this Type type, string namespaceFragment)
        {
            return type.Namespace != null &&
                  (type.Namespace.EndsWith("." + namespaceFragment) || type.Namespace.Contains("." + namespaceFragment + "."));
        }
    }
}
