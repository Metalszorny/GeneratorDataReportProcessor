using Common.Enums;
using System;

namespace Common.Interfaces
{
    public interface IReportProcessorEngine : IDisposable
    {
        #region Properties

        ReportProcessorEngineStateTypes State
        { get; set; }

        #endregion Properties

        #region Methods

        void Start();

        #endregion Methods
    }
}
