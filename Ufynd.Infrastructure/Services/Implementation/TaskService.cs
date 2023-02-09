using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FastMember;
using Ufynd.Data.Repositories;
using Ufynd.Infrastructure.Models;
using OfficeOpenXml;
using Ufynd.Infrastructure.Helpers;

namespace Ufynd.Infrastructure.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly IHotelRepository _hotelRepository;

        public TaskService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task TaskOne()
        {
            throw new System.NotImplementedException();
        }

        public async Task TaskTwo()
        {
            var table = new DataTable();
            var data = _hotelRepository.GetDataForExport();
            var excelData = new List<ExcelExportData>();
            foreach (var s in data.HotelRates)
            {
                var toAdd = new ExcelExportData()
                {
                    ARRIVAL_DATE = s.TargetDay.Date.Format(),
                    DEPARTURE_DATE = HotelHelper.GetDepartureDate(s.Los, s.TargetDay).Format(),
                    PRICE = s.Price.NumericFloat,
                    CURRENCY = s.Price.Currency,
                    RATENAME = s.RateName,
                    ADULTS = s.Adults
                };

                var breakFastTag = s.RateTags.FirstOrDefault(x => x.Name == "breakfast");
                if (breakFastTag != null)
                {
                    toAdd.BREAKFAST_INCLUDED = breakFastTag.Shape ? 1 : 0;
                }
                excelData.Add(toAdd);
            }

            using (var reader = ObjectReader.Create(excelData))
            {
                table.Load(reader);
            }
            
            var fileInfo = new FileInfo("task2.xlsx");
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
                fileInfo = new FileInfo("task2.xlsx");
            }
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var pkg = new ExcelPackage(fileInfo))
            {
                var workSheet = pkg.Workbook.Worksheets.Add("Hostel report");
                workSheet.Cells["A1"].LoadFromDataTable(table, PrintHeaders:true);
                await pkg.SaveAsync();
            }
        }

        public  BaseModel TaskThree(int hotelId, string arrivalDate)
        {
            return _hotelRepository.Get(hotelId, arrivalDate);
        }
    }
}