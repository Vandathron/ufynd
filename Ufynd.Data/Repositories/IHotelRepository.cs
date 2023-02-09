using Ufynd.Infrastructure.Models;

namespace Ufynd.Data.Repositories
{
    public interface IHotelRepository
    {
        BaseModel Get(int hotelId, string arrivalDate);

        BaseModel GetDataForExport();
    }
}