using System;

namespace Common.Models
{
    public sealed class Day
    {
        #region Properties

        public string Name
        { get; set; }

        public DateTime Date
        { get; set; }

        public double Emission
        { get; set; }

        public double Energy
        { get; set; }

        public double Price
        { get; set; }

        #endregion Properties
    }
}
