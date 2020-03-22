using Common.Models;

namespace Common.Interfaces
{
    public interface IFossilFuelGenerator
    {
        #region Properties

        double EmissionsRating
        { get; set; }

        #endregion Properties

        #region Methods

        void CalculateDailyEmission(EmissionsFactor emissionsFactor);

        #endregion Methods
    }
}
