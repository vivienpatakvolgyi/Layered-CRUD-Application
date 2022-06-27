using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace D2P0JX_SG1_21_22_2.Models.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int PizzaId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual Pizza Pizza { get; set; }
    }
}
