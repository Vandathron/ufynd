using System;
using System.Collections.Generic;

namespace Ufynd.Infrastructure.Models
{
    public class HotelRateModel
    {
        public int Adults { get; set; }
        public int Los { get; set; }
        public string RateDescription { get; set; }
        public int RateID { get; set; }
        public string RateName { get; set; }
        public DateTime TargetDay { get; set; }
        public PriceModel Price { get; set; }
        public List<RateTagModel> RateTags { get; set; }
    }
}