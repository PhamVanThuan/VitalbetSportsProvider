using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace VitalbetSportsProvider.Models.Xml
{
    [Serializable]
    [DebuggerDisplay("{Name}({Id}) Events = {Events?.Count ?? 0}")]
    public class XmlSport
    {
        [XmlAttribute("ID")]
        public ushort Id { get; set; }
        
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("Event")]
        public List<XmlEvent> Events { get; set; }
    }
}