using Common.Models;

namespace Common.Interfaces
{
    public interface IGenerator
    {
        #region Properties

        string Name
        { get; set; }

        Day[] Generation
        { get; set; }

        #endregion Properties

        #region Methods

        double CalculateTotalGenerationValue(ValueFactor valueFactor);

        #endregion Methods
    }
}
