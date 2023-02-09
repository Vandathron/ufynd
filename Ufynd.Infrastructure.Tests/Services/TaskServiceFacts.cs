using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Ufynd.Data.Repositories;
using Ufynd.Infrastructure.Models;
using Ufynd.Infrastructure.Services;
using Ufynd.Infrastructure.Services.Implementation;

namespace Ufynd.Infrastructure.Tests.Services
{
    public class TaskServiceFacts
    {
        private ITaskService _taskService;
        private Mock<IHotelRepository> _hotelRepository;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _hotelRepository = new Mock<IHotelRepository>();
            _taskService = new TaskService(_hotelRepository.Object);
        }

        [Test]
        public void TaskThree_ShouldReturnNull_WhenHotelDoesNotExists()
        {
            // Arrange
            var hotelId = 2;
            var arrivalDate = DateTime.Now.ToString();
            _hotelRepository.Setup(x => x.Get(hotelId, arrivalDate)).Returns(new BaseModel()
            {
                Hotel = new HotelModel()
                {
                    HotelID = hotelId,
                    Name = "hotel test"
                },
                HotelRates = new List<HotelRateModel>()
                {
                    new HotelRateModel(){Adults = 2, TargetDay = DateTime.Parse(arrivalDate)}
                }
            });
            
            // Act
            var result = _taskService.TaskThree(2, DateTime.Parse(arrivalDate).AddDays(1).ToString());
            
            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task TaskTwo_ShouldGenerateAFile()
        {
            // Arrange
            _hotelRepository.Setup(x => x.GetDataForExport()).Returns(new BaseModel()
            {
                Hotel = new HotelModel()
                {
                    HotelID = It.IsAny<int>(),
                    Name = "hotel test"
                },
                HotelRates = new List<HotelRateModel>()
                {
                    new HotelRateModel(){Adults = 2, TargetDay = DateTime.Now, Price = new PriceModel(){Currency = "EUR", NumericFloat = 2.2}, RateTags = new List<RateTagModel>()
                    {
                        new RateTagModel() {Name = "breakfast", Shape = true}
                    }}
                }
            });
            
            // Act
            var file = new FileInfo("task2.xlsx");
            if(file.Exists) file.Delete();
            await _taskService.TaskTwo();
            file = new FileInfo("task2.xlsx");
            // Assert
            Assert.IsTrue(file.Exists);
        }
    }
}