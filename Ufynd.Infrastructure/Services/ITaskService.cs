using System.Threading.Tasks;
using Ufynd.Infrastructure.Models;

namespace Ufynd.Infrastructure.Services
{
    public interface ITaskService
    {
        Task TaskOne();
        Task TaskTwo();
        BaseModel TaskThree(int hotelId, string arrivalDate);
    }
}