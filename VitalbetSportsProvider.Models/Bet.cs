using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace VitalbetSportsProvider.Models
{
    [Table("Bets")]
    [DebuggerDisplay("{Name}({Id}) Odds = {Odds?.Count ?? 0}")]
    public class Bet
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(64)]
        public string Name { get; set; }

        public int MatchId { get; set; }

        public List<Odds> Odds { get; set; } = new List<Odds>();

        public bool IsLive { get; set; }
    }
}