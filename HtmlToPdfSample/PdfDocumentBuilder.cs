using System;
using System.Drawing.Printing;
using TuesPechkin;

namespace HtmlToPdfSample
{
    public class PdfDocumentBuilder
    {
        private string _html;
        private PaperKind _paperSize;
        private GlobalSettings.DocumentOutputFormat _outputFormat;
        private string _title;


        public PdfDocumentBuilder(string html)
        {
            _html = html;
        }

        public PdfDocumentBuilder WithPageSize(PaperKind paperSize)
        {
            _paperSize = paperSize;
            return this;
        }

        public PdfDocumentBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public PdfDocumentBuilder WithOutputFormat(GlobalSettings.DocumentOutputFormat outputFormat)
        {
            _outputFormat = outputFormat;
            return this;
        }

        public IDocument Build()
        {
            var screenScaleFactor = NativeDeviceHelper.GetScalingFactor();

            var doc = new HtmlToPdfDocument(_html)
            {
                GlobalSettings =
                {
                    PaperSize = _paperSize,
                    OutputFormat = _outputFormat,
                    DocumentTitle = _title,
                }   
            };

            // By default this version of wkhtmltopdf uses the native 
            // DPI, which means PDF's will be zoomed out on high DPI
            // machines and normal on 100% scaled machines. Changing
            // the zoom factor to the discovered screen scale factor
            // ensures the generation is consistent no matter what 
            // machine the template is developed on.
            foreach (var obj in doc.Objects)
            {
                obj.LoadSettings.ZoomFactor = screenScaleFactor;
            }

            return doc;
        }
    }
}