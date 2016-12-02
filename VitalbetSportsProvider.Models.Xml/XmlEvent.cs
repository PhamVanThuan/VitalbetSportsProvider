namespace VitalbetSportsProvider.Models.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable]
    [DebuggerDisplay("{Name}({Id}) Matches = {Matches?.Count ?? 0}")]
    public class XmlEvent
    {
        [XmlAttribute("ID")]
        public ushort Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("Match")]
        public List<XmlMatch> Matches { get; set; }

        [XmlAttribute]
        public bool IsLive { get; set; }
    
        [XmlAttribute("CategoryID")]
        public ushort CategoryId { get; set; }
    }
}