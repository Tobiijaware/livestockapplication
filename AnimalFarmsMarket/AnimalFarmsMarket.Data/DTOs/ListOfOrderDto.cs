using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalFarmsMarket.Data.DTOs
{
    public class ListOfOrderDto
    {
        public string Id { get; set; }

        public byte Status { get; set; }

        public string TrackingNumber { get; set; }

        public byte ConfirmationStatus { get; set; }

        public string DateUpdated { get; set; }
    }
}
