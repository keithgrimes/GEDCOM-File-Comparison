using System;
using GEDCOM;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace GEDCOM_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load the configuration
            //CONFIG appConfig = new($"appsettings.json");
            if (args.Length < 1)
            {
                Console.WriteLine("Please provide the configuration file as the first argument.");
                return;
            }
            CONFIG appConfig = new(args[0]);
            List<GEDCOM.Comparison> comp = [];

            // Load each comparison setting up.
            foreach (CONFIGComparisonFile file in appConfig.comparisonConfiguration)
            {
                comp.Add(new Comparison(appConfig.masterConfiguration, file, appConfig.baseConfiguration));
            }

            // Now we have defined all the comparisons to run, 
            // Exceute them!
            foreach (GEDCOM.Comparison file in comp)
            {
                StringBuilder report = new ();
                // Compare and generate the verbose report.
                file.Compare(report);
                file.Report(report);

                report = null;
            }
        }
    }
}
