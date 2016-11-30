using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace VitalbetSportsProvider.Models.Xml
{
    [Serializable]
    [DebuggerDisplay("{Name}({Id}) Odds = {Odds?.Count ?? 0}")]
    public class XmlBet
    {
        [XmlAttribute("ID")]
        public uint Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("Odd")]
        public List<XmlOdds> Odds { get; set; }

        [XmlAttribute]
        public bool IsLive { get; set; }
    }
}