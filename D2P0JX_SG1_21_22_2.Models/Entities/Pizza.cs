using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace D2P0JX_SG1_21_22_2.Models.Entities
{
    [Table("Pizzas")]
    public class Pizza
    {
       

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Price { get; set; }
        public int Size { get; set; }
        public bool IsGlutenFree { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

    }
}
