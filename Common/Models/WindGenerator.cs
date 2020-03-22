using Common.Interfaces;
using System;
using System.Linq;

namespace Common.Models
{
    public sealed class WindGenerator : IWindGenerator
    {
        #region Properties

        public string Name
        { get; set; }

        public Day[] Generation
        { get; set; }

        public string Location
        { get; set; }

        #endregion Properties

        #region Methods

        public double CalculateTotalGenerationValue(ValueFactor valueFactor)
        {
            return Generation.Sum(x => x.Energy * x.Price * (this.IsOffshore() ? valueFactor.Low : valueFactor.High));
        }

        private bool IsOffshore()
        {
            return string.Equals(this.Location, "Offshore", StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion Methods
    }
}
