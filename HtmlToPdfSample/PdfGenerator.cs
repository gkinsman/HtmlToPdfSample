using TuesPechkin;

namespace HtmlToPdfSample
{
    public interface IPdfGenerator
    {
        byte[] Generate(IDocument document);
    }

    public class PdfGenerator : IPdfGenerator
    {
        private readonly ThreadSafeConverter _converter;

        public PdfGenerator()
        {
            _converter = new ThreadSafeConverter(
                new RemotingToolset<PdfToolset>(
                    new WinAnyCPUEmbeddedDeployment(
                        new TempFolderDeployment())));
        }

        public byte[] Generate(IDocument document)
        {
            return _converter.Convert(document);
        }
    }
}