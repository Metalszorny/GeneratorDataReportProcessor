using Common.Models;

namespace Common.Interfaces
{
    public interface IConfigurationData
    {
        #region Properties

        string InputFolder
        { get; set; }

        string OutputFolder
        { get; set; }

        ReferenceData ReferenceData
        { get; set; }

        #endregion Properties
    }
}
