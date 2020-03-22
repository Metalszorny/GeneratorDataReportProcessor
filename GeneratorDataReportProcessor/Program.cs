using Common.Enums;
using Common.Interfaces;
using Common.Models;
using System;
using System.Threading;

namespace GeneratorDataReportProcessor
{
    /// <summary>
    /// Interaction logic for Program.
    /// </summary>
    class Program
    {
        #region Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Console line arguments.</param>
        static void Main(string[] args)
        {
            IConfigurationData configurationData;

            try
            {
                // Get the configuration data and the reference data.
                configurationData = new ConfigurationData();

                // Call the report processing engine.
                using (var reportProcessorEngine = new ReportProcessorEngine(configurationData))
                {
                    reportProcessorEngine.Start();

                    // While the engine is working, don't close the application.
                    while (reportProcessorEngine.State != ReportProcessorEngineStateTypes.Stopped)
                    {
                        Thread.Sleep(50);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        #endregion Methods
    }
}
