using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimalFarmsMarket.Data.Models
{
    public class Agent : BaseEntity
    {
        [Required]
        public string BusinessLocation { get; set; }

        [Required]
        public string NIN { get; set; }

        [Required]
        public string NINTrackingId { get; set; }

        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public ICollection<Livestock> Livestocks { get; set; }

        public Agent()
        {
            Livestocks = new HashSet<Livestock>();
        }

    }
}
