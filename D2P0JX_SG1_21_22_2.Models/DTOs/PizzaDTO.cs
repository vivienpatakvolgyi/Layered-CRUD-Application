namespace D2P0JX_SG1_21_22_2.Models.DTOs
{
    public class PizzaDTO
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Size { get; set; }
        public bool IsGlutenFree { get; set; }

        public string Name { get; set; }
        public int RestaurantId { get; set; }
    }
}
