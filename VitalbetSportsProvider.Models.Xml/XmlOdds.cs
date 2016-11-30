using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace VitalbetSportsProvider.Models.Xml
{
    [Serializable]
    [DebuggerDisplay("{Name}({Id}) Value = {Value}")]
    public class XmlOdds
    {
        [XmlAttribute("ID")]
        public uint Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }
        
        [XmlAttribute]
        public decimal Value { get; set; }
        
        [XmlAttribute]
        public decimal SpecialBetValue { get; set; }
        
        [XmlIgnore]
        public bool SpecialBetValueSpecified { get; set; }
    }
}