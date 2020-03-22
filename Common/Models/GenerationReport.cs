using Common.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Common.Models
{
    public sealed class GenerationReport
    {
        #region Properties

        public WindGenerator[] Wind
        { get; set; }

        public GasGenerator[] Gas
        { get; set; }

        public CoalGenerator[] Coal
        { get; set; }

        #endregion Properties

        #region Methods

        public IEnumerable<IGenerator> GetAllGenerators()
        {
            var generators = new List<IGenerator>();
            generators.AddRange(this.Coal);
            generators.AddRange(this.Gas);
            generators.AddRange(this.Wind);
            return generators;
        }

        public IEnumerable<Day> GetEmissionDays(EmissionsFactor emissionsFactor)
        {
            var days = new List<Day>();
            var generators = GetAllGenerators().Where(x => x is ICoalGenerator || x is IGasGenerator);

            foreach (var generator in generators)
            {
                // Calculate emissions.
                if (generator is ICoalGenerator)
                {
                    ((ICoalGenerator)generator).CalculateDailyEmission(emissionsFactor);
                }
                else if (generator is IGasGenerator)
                {
                    ((IGasGenerator)generator).CalculateDailyEmission(emissionsFactor);
                }

                days.AddRange(generator.Generation.Select(x => new Day()
                {
                    Date = x.Date,
                    Energy = x.Energy,
                    Name = generator.Name,
                    Price = x.Price,
                    Emission = x.Emission
                }));
            }

            return days;
        }

        #endregion Methods
    }
}
