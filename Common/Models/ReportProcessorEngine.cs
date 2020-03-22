using Common.Enums;
using Common.Interfaces;
using Common.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Common.Models
{
    /// <summary>
    /// Interaction logic for the report processing engine.
    /// </summary>
    public sealed class ReportProcessorEngine : IReportProcessorEngine
    {
        #region Properties

        /// <summary>
        /// Gets or sets the state of the report processor engine.
        /// </summary>
        /// <value>
        /// The new value of the report processor engine state.
        /// </value>
        public ReportProcessorEngineStateTypes State
        { get; set; }

        /// <summary>
        /// Gets or sets the value of the main timer.
        /// </summary>
        /// <value>
        /// The new value of the main timer.
        /// </value>
        private Timer MainTimer
        { get; set; }

        /// <summary>
        /// Gets or sets the value of the configuration data.
        /// </summary>
        /// <value>
        /// The new value of the configuration data.
        /// </value>
        private IConfigurationData ConfigurationData
        { get; set; }

        /// <summary>
        /// Gets or sets the value of the thread locking object.
        /// </summary>
        /// <value>
        /// The new value of the thread locking object.
        /// </value>
        private object LockObject
        { get; set; }

        /// <summary>
        /// Gets or sets the value of the task list.
        /// </summary>
        /// <value>
        /// The new value of the task list.
        /// </value>
        private List<Task> TaskList
        { get; set; }

        /// <summary>
        /// Gets or sets the value of the file names under process.
        /// </summary>
        /// <value>
        /// The new value of the file names under process.
        /// </value>
        private List<string> ItemsInProcess
        { get; set; }

        #endregion Preoperties

        #region Constructors

        /// <summary>
        /// Creates an instance of the <see cref="ReportProcessorEngine"/> class.
        /// </summary>
        /// <param name="configurationData">The configuration data.</param>
        public ReportProcessorEngine(IConfigurationData configurationData)
        {
            this.ConfigurationData = configurationData;
            this.InitializeComponent();
        }

        /// <summary>
        /// Destroys an instance of the <see cref="ReportProcessorEngine"/> class.
        /// </summary>
        ~ReportProcessorEngine()
        { }

        #endregion Constructors

        #region Methods

        #region Public Methods

        /// <summary>
        /// Disposes the engine.
        /// </summary>
        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        /// <summary>
        /// Starts the engine.
        /// </summary>
        public void Start()
        {
            this.MainTimer.Enabled = true;
            this.MainTimer.Start();
            Console.WriteLine(string.Format("Application started at {0}, press Escape to close the application.", DateTime.Now));
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Stops the engine.
        /// </summary>
        private void Stop()
        {
            this.MainTimer.Stop();
            this.MainTimer.Enabled = false;
            this.MainTimer.Elapsed -= this.TimerTick;
            this.MainTimer = null;
            this.State = ReportProcessorEngineStateTypes.Stopped;
            this.TaskList = null;
            this.ItemsInProcess = null;
            this.LockObject = null;
        }

        /// <summary>
        /// Initializes the components.
        /// </summary>
        private void InitializeComponent()
        {
            this.LockObject = new object();
            this.ItemsInProcess = new List<string>();
            this.TaskList = new List<Task>();
            this.State = ReportProcessorEngineStateTypes.Starting;
            this.MainTimer = new Timer();
            this.MainTimer.Interval = 250;
            this.MainTimer.Elapsed += TimerTick;
        }

        /// <summary>
        /// The tick method of the timer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void TimerTick(object sender, ElapsedEventArgs e)
        {
            var outputFiles = this.SearchOutputFolderForReportFiles();
            var inputFiles = this.SearchInputFolderForReportFiles();
            inputFiles = this.FilterInputFiles(inputFiles, outputFiles);

            // If there are files to process, add start independent tasks.
            if (inputFiles.Any())
            {
                Console.WriteLine(string.Format("Found {0} input file started processing them at {1}.", inputFiles.Count(), DateTime.Now));

                foreach (var fileName in inputFiles)
                {
                    lock (this.LockObject)
                    {
                        this.ItemsInProcess.Add(this.FormatFileName(fileName));
                        var newTask = new Task(() =>
                        {
                            this.ProcessInputFile(fileName);
                        });
                        newTask.Start();
                        this.TaskList.Add(newTask);
                    }
                }
            }

            lock (this.LockObject)
            {
                // Remove the tasks that are already completed.
                for (int i = 0; i < this.TaskList.Count; i++)
                {
                    if (this.TaskList[i].IsCompleted)
                    {
                        this.TaskList.Remove(this.TaskList[i]);
                        i--;
                    }
                }
            }
        }

        /// <summary>
        /// Filters out any files matching between the input and output folder to prevent data loss.
        /// </summary>
        /// <param name="inputFiles">The files in the input folder.</param>
        /// <param name="outputFiles">The files in the output folder.</param>
        /// <returns>The input files that can start processing.</returns>
        private IEnumerable<string> FilterInputFiles(IEnumerable<string> inputFiles, IEnumerable<string> outputFiles)
        {
            if (!outputFiles.Any())
            {
                return inputFiles;
            }

            outputFiles = outputFiles.Select(x => this.FormatFileName(x));
            inputFiles = inputFiles.Where(x => 
                !outputFiles.Contains(this.FormatFileName(x)) &&
                !this.ItemsInProcess.Contains(this.FormatFileName(x)));

            return inputFiles;
        }

        /// <summary>
        /// Formats the file name to be a simple file name without extension and folder path.
        /// </summary>
        /// <param name="inputFileName">The file name to be formatted.</param>
        /// <returns>The formatted simple file name.</returns>
        private string FormatFileName(string inputFileName)
        {
            string returnValue = inputFileName;

            if (returnValue.Contains(this.ConfigurationData.InputFolder) &&
                returnValue.ToLower().Contains(".xml"))
            {
                returnValue = new FileInfo(returnValue).Name.Replace(".xml", "");
            }

            if (returnValue.Contains(this.ConfigurationData.OutputFolder) &&
                returnValue.ToLower().Contains(".xml"))
            {
                returnValue = new FileInfo(returnValue).Name.Replace(".xml", "");
            }

            return returnValue;
        }

        /// <summary>
        /// Processes the data from the input file.
        /// </summary>
        /// <param name="inputFile">The name of the input file.</param>
        private void ProcessInputFile(string fileName)
        {
            Console.WriteLine(string.Format("Processing {0} input file started at {1}.", fileName, DateTime.Now));
            var generatorReport = this.GetReportData(fileName);

            if (generatorReport != null)
            {
                var generationOutput = this.CreateGenerationOutput(generatorReport);
                this.CreateOutputFile(fileName, generationOutput);
            }

            Console.WriteLine(string.Format("Processing {0} input file ended at {1}.", fileName, DateTime.Now));
        }

        /// <summary>
        /// Gets the file names from the input folder.
        /// </summary>
        /// <returns>The file names from the input folder.</returns>
        private IEnumerable<string> SearchInputFolderForReportFiles()
        {
            return Directory.GetFiles(this.ConfigurationData.InputFolder, "*.xml", SearchOption.AllDirectories);
        }

        /// <summary>
        /// Gets the file names from the output folder.
        /// </summary>
        /// <returns>The file names from the output folder.</returns>
        private IEnumerable<string> SearchOutputFolderForReportFiles()
        {
            return Directory.GetFiles(this.ConfigurationData.OutputFolder, "*.xml", SearchOption.AllDirectories);
        }

        /// <summary>
        /// Deserializes the the xml data of the report.
        /// </summary>
        /// <param name="fileName">The input file path.</param>
        /// <returns>The deserialized xml data of the report.</returns>
        private GenerationReport GetReportData(string fileName)
        {
            return XmlSerializerManager.DeserializeToObject<GenerationReport>(File.ReadAllBytes(fileName), Encoding.UTF8);
        }

        /// <summary>
        /// Generates the output data.
        /// </summary>
        /// <param name="generatorReport">The generator report.</param>
        /// <returns>The generated output data.</returns>
        private GenerationOutput CreateGenerationOutput(GenerationReport generatorReport)
        {
            var generationOutput = new GenerationOutput();            
            generationOutput.Totals = this.GetTotals(generatorReport).ToArray();
            generationOutput.MaxEmissionGenerators = this.GetEmissionData(generatorReport).ToArray();
            generationOutput.ActualHeatRates = this.GetActualHeatRates(generatorReport).ToArray();

            return generationOutput;
        }

        /// <summary>
        /// Gets the total generation values.
        /// </summary>
        /// <param name="generatorReport">The generator report.</param>
        /// <returns>The total generation values.</returns>
        private IEnumerable<Generator> GetTotals(GenerationReport generatorReport)
        {
            var returnValue = new List<Generator>();
            var generators = generatorReport.GetAllGenerators();

            foreach (var generator in generators)
            {
                returnValue.Add(new Generator()
                {
                    Name = generator.Name,
                    Total = generator.CalculateTotalGenerationValue((ValueFactor)this.ConfigurationData.ReferenceData.Factors.FirstOrDefault(x => x is ValueFactor))
                });
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the highest daily emissions.
        /// </summary>
        /// <param name="generatorReport">The generator report.</param>
        /// <returns>The highest daily emissions.</returns>
        private IEnumerable<OutputDay> GetEmissionData(GenerationReport generatorReport)
        {
            var days = generatorReport.GetEmissionDays((EmissionsFactor)this.ConfigurationData.ReferenceData.Factors
                .FirstOrDefault(x => x is EmissionsFactor)).OrderBy(x => x.Date).ThenByDescending(x => x.Emission);
            var returnValue = (from element in days
                               group element by element.Date
                               into groups
                               select groups.OrderBy(x => x.Date).First()).ToArray();

            return returnValue.Select(x => new OutputDay()
            {
                Name = x.Name,
                Date = x.Date,
                Emission = x.Emission
            });
        }

        /// <summary>
        /// Gets the actual heat rates.
        /// </summary>
        /// <param name="generatorReport">The generator report.</param>
        /// <returns>The actual heat rates.</returns>
        private IEnumerable<ActualHeatRate> GetActualHeatRates(GenerationReport generatorReport)
        {
            var returnValue = new List<ActualHeatRate>();

            foreach (var coalGenerator in generatorReport.Coal)
            {
                returnValue.Add(new ActualHeatRate()
                {
                    Name = coalGenerator.Name,
                    HeatRate = coalGenerator.CalculateActualHeatRate()
                });
            }

            return returnValue;
        }

        /// <summary>
        /// Writes the generated output to a file named after the input file name.
        /// </summary>
        /// <param name="fileName">The input files name.</param>
        /// <param name="generationOutput">The output data.</param>
        private void CreateOutputFile(string fileName, GenerationOutput generationOutput)
        {
            if (fileName.Contains(this.ConfigurationData.InputFolder) &&
                fileName.ToLower().Contains(".xml"))
            {
                fileName = new FileInfo(fileName).Name.Replace(".xml", "");
            }

            lock (this.LockObject)
            {
                using (var stream = File.Create(this.ConfigurationData.OutputFolder + fileName + ".xml"))
                {
                    using (var streamWriter = new StreamWriter(stream))
                    {
                        var content = XmlSerializerManager.SerializeToXmlString(generationOutput, Encoding.UTF8);
                        streamWriter.Write(content);
                    }
                }

                this.ItemsInProcess.Remove(fileName);
            }
        }

        #endregion Private Methods

        #endregion Methods
    }
}
