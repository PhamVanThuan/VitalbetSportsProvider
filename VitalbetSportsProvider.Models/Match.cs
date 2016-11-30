using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace VitalbetSportsProvider.Models
{
    [Table("Matches")]
    [DebuggerDisplay("{Name}({Id}) Bets = {Bets?.Count ?? 0}")]
    public class Match
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public int EventId { get; set; }

        public List<Bet> Bets { get; set; } = new List<Bet>();

        public DateTime StartDate { get; set; }
    
        public MatchType MatchType { get; set; }
    }
}