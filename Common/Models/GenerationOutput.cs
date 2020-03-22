namespace Common.Models
{
    public sealed class GenerationOutput
    {
        #region Properties

        public Generator[] Totals
        { get; set; }

        public OutputDay[] MaxEmissionGenerators
        { get; set; }

        public ActualHeatRate[] ActualHeatRates
        { get; set; }

        #endregion Properties
    }
}
