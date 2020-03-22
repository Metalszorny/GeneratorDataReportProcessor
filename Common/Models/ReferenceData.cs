using System.Xml.Serialization;

namespace Common.Models
{
    public sealed class ReferenceData
    {
        #region Properties

        [XmlArrayItem("ValueFactor", typeof(ValueFactor))]
        [XmlArrayItem("EmissionsFactor", typeof(EmissionsFactor))]
        public BaseFactor[] Factors
        { get; set; }

        #endregion Properties
    }
}
