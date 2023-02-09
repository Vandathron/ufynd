using System.Collections.Generic;

namespace Ufynd.Infrastructure.Models
{
    public class BaseModel
    {
        public HotelModel Hotel { get; set; }
        public List<HotelRateModel> HotelRates { get; set; }
    }
}