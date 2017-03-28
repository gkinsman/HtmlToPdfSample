using System.IO;
using System.Linq;
using System.Reflection;

namespace HtmlToPdfSample
{
    public static class AssemblyExtensions
    {
        public static string GetEmbeddedResource(this Assembly assembly, string resourceName)
        {
            var embeddedResources = assembly.GetManifestResourceNames();
            var embeddedResourceName =
                    embeddedResources.Single(resource => resource.EndsWith($"{resourceName}.cshtml"));

            using (var stream = assembly.GetManifestResourceStream(embeddedResourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }

        }
    }
}