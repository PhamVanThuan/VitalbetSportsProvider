using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace VitalbetSportsProvider.Models.Xml
{
    [Serializable]
    [DebuggerDisplay("{Name}({Id}) Bets = {Bets?.Count ?? 0}")]
    public class XmlMatch
    {
        [XmlAttribute("ID")]
        public uint Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("Bet")]
        public List<XmlBet> Bets { get; set; }

        [XmlAttribute]
        public DateTime StartDate { get; set; }
    
        [XmlAttribute]
        public string MatchType { get; set; }
    }
}