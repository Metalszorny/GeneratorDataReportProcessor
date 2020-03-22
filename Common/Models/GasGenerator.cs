using Common.Interfaces;
using System.Linq;

namespace Common.Models
{
    public sealed class GasGenerator : IGasGenerator
    {
        #region Properties

        public string Name
        { get; set; }

        public Day[] Generation
        { get; set; }

        public double EmissionsRating
        { get; set; }

        #endregion Properties

        #region Methods

        public double CalculateTotalGenerationValue(ValueFactor valueFactor)
        {
            return Generation.Sum(x => x.Energy * x.Price * valueFactor.Medium);
        }

        public void CalculateDailyEmission(EmissionsFactor emissionsFactor)
        {
            foreach (var day in Generation)
            {
                day.Emission = day.Energy * this.EmissionsRating * emissionsFactor.Medium;
            }
        }

        #endregion Methods
    }
}
