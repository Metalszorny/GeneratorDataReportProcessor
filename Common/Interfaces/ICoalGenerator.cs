namespace Common.Interfaces
{
    public interface ICoalGenerator : IGenerator, IFossilFuelGenerator
    {
        #region Properties

        double TotalHeatInput
        { get; set; }

        double ActualNetGeneration
        { get; set; }

        #endregion Properties

        #region Methods

        double CalculateActualHeatRate();

        #endregion Methods
    }
}
