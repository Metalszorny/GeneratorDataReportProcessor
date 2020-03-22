using Common.Exceptions;
using Common.Interfaces;
using Common.Managers;
using System.IO;
using System.Text;

namespace Common.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConfigurationData : IConfigurationData
    {
        #region Properties

        public string InputFolder
        { get; set; }

        public string OutputFolder
        { get; set; }

        public ReferenceData ReferenceData
        { get; set; }

        #endregion Properties

        #region Constructors

        public ConfigurationData()
        {
            this.ReadConfigurationData();
        }

        ~ConfigurationData()
        { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        private void ReadConfigurationData()
        {
            var inputFolder = ConfigurationAccessManager.GetApplicationSetting("InputFolder");

            if (!Directory.Exists(inputFolder))
            {
                throw new InputFolderNotExistsException();
            }

            this.InputFolder = inputFolder;
            var outputFolder = ConfigurationAccessManager.GetApplicationSetting("OutputFolder");

            if (!Directory.Exists(outputFolder))
            {
                throw new OutputFolderNotExistsException();
            }

            this.OutputFolder = outputFolder;
            var referenceFile = ConfigurationAccessManager.GetApplicationSetting("ReferenceFile");

            if (File.GetAttributes(referenceFile).HasFlag(FileAttributes.Directory)
                || !File.Exists(referenceFile))
            {
                throw new ReferenceFileNotExistsException();
            }

            this.ReferenceData = XmlSerializerManager.DeserializeToObject<ReferenceData>(File.ReadAllBytes(referenceFile), Encoding.UTF8);

            if (this.ReferenceData == null || this.ReferenceData.Factors == null)
            {
                throw new ReferenceFileSerializationException();
            }
        }

        #endregion Methods
    }
}
