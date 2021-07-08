using AnimalFarmsMarket.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalFarmsMarket.Data.DTOs
{
    public class LivestockToReturnDto
    {
        public string Id { get; set; }

        public decimal SellingPrice { get; set; }

        public IEnumerable<LivestockImages> Images { get; set; }
    }
}
