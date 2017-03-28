using CLAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlToPdfSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var generatorApp = new GeneratorApp();

            try
            {
                Parser.RunConsole(args, generatorApp);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
    }
}
