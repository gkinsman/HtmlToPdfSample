using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace HtmlToPdfSample
{
    public interface IHtmlGenerator
    {
        string Generate<T>(T templateModel, string htmlTemplateName);
    }

    public class HtmlGenerator : IHtmlGenerator
    {
        private readonly IRazorEngineService _service;

        public HtmlGenerator()
        {
            var assembly = typeof(HtmlGenerator).Assembly;

            var config = new TemplateServiceConfiguration
            {
                TemplateManager = new DelegateTemplateManager(resourceName => assembly.GetEmbeddedResource(resourceName)),
                CompilerServiceFactory = new RazorEngine.Roslyn.RoslynCompilerServiceFactory()
            };

            _service = RazorEngineService.Create(config);
        }

        public string Generate<T>(T templateModel, string htmlTemplateName)
        {
            return _service.RunCompile(htmlTemplateName, typeof(T), templateModel);
        }
    }
}
