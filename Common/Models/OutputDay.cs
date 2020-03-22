using System;
using System.Xml.Serialization;

namespace Common.Models
{
    [XmlType(TypeName = "Day")]
    public sealed class OutputDay
    {
        #region Properties

        public string Name
        { get; set; }

        public DateTime Date
        { get; set; }

        public double Emission
        { get; set; }

        #endregion Properties
    }
}
