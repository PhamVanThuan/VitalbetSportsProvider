using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace VitalbetSportsProvider.Models
{
    [Table("Odds")]
    [DebuggerDisplay("{Name}({Id}) Value = {Value}")]
    public class Odds
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        public int BetId { get; set; }

        public decimal Value { get; set; }
        
        public decimal SpecialBetValue { get; set; }
        
        public bool SpecialBetValueSpecified { get; set; }
    }
}