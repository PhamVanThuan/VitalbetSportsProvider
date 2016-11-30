using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace VitalbetSportsProvider.Models
{
    [Table("Events")]
    [DebuggerDisplay("{Name}({Id}) Matches = {Matches?.Count ?? 0}")]
    public class Event
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public int SportId { get; set; }

        public List<Match> Matches { get; set; } = new List<Match>();

        public bool IsLive { get; set; }
    
        public int CategoryId { get; set; }
    }
}