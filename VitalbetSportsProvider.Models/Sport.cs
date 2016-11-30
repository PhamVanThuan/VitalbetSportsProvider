using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace VitalbetSportsProvider.Models
{
    [Table("Sports")]
    [DebuggerDisplay("{Name}({Id}) Events = {Events?.Count ?? 0}")]
    public class Sport
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

        public List<Event> Events { get; set; } = new List<Event>();
    }
}