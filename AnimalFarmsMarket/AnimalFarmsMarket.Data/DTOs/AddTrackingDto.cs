using System.ComponentModel.DataAnnotations;


namespace AnimalFarmsMarket.Data.DTOs
{
   public class AddTrackingDto
    {
        [Required]
        public byte Status { get; set; }

        [Required]
        public string OrderId { get; set; }

        [Required]
        public string DeliveryPersonId { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
