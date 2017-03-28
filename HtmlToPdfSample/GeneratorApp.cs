using System;
using System.Collections.Generic;
using System.IO;
using CLAP;
using HtmlToPdfSample.Templates.PersonsFavouriteCities;

namespace HtmlToPdfSample
{
    public class GeneratorApp
    {
        private readonly HtmlGenerator _htmlGenerator;
        private readonly PdfGenerator _pdfGenerator;

        public GeneratorApp()
        {
            // Should be registered with IoC as a singleton to 
            // ensure the same engine/template manager/cache is used
            _htmlGenerator = new HtmlGenerator();

            // Should be registered with IoC as a singleton to
            // ensure the same converter is used. In IIS environments
            // it will ensure single-threaded operation by using a
            // queue, which makes being a single instance critical.
            _pdfGenerator = new PdfGenerator();
        }

        [Verb(IsDefault = true)]
        public void Generate(string pdfName)
        {
            if (pdfName == null) throw new ArgumentNullException(nameof(pdfName));

            switch (pdfName)
            {
                case "FavouriteCities":
                    GenerateFavouriteCitiesPdf();
                    break;
                default:
                    throw new InvalidOperationException($"No pdf with name {pdfName} found");
            }
        }

        private void GenerateFavouriteCitiesPdf()
        {
            var model = new PersonsFavouriteCitiesModel("Isaac Asimov", new DateTime(1919, 10, 4), "Toronto",
                                                        "Philadelphia", "Boston", "New York City");

            // Apply model to razor template
            var html = _htmlGenerator.Generate(model, "PersonsFavouriteCities");

            // Build PDF object model
            var pdfDocument = new PdfDocumentBuilder(html)
                    .WithPageSize(System.Drawing.Printing.PaperKind.A4)
                    .WithTitle(model.Name)
                    .Build();

            // Render to byte array
            var pdf = _pdfGenerator.Generate(pdfDocument);

            // Write to file
            using (var fileStream = new FileStream($"{model.Name}-{Guid.NewGuid()}.pdf", FileMode.Create))
            {
                fileStream.Write(pdf, 0, pdf.Length);
            }
        }
    }
}
