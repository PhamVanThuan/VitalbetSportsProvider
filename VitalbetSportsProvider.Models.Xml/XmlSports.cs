namespace VitalbetSportsProvider.Models.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable]
    [DebuggerDisplay("CreateDate = {CreateDate} Sports = {Sports?.Count ?? 0}")]
    public class XmlSports
    {
        [XmlElement("Sport")]
        public List<XmlSport> Sports { get; set; }
    
        [XmlAttribute]
        public DateTime CreateDate { get; set; }
    }
}