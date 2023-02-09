using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ufynd.Infrastructure.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ufynd.Data.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private Dictionary<int, BaseModel> Store;
        private BaseModel DataForExport;
        public HotelRepository()
        {
            Store = new Dictionary<int, BaseModel>();
            LoadData();
        }
        
        public BaseModel Get(int hotelId, string arrivalDate)
        {
            var date = DateTime.Parse(arrivalDate).Date;
            var result = new BaseModel();
            try
            {
                var hotel = Store[hotelId];
                result.Hotel = hotel.Hotel;
                result.HotelRates = new List<HotelRateModel>();
                foreach (var r in hotel.HotelRates)
                {
                    if (r.TargetDay.Date == date)
                    {
                        result.HotelRates.Add(r);
                    }
                }
            }
            catch (KeyNotFoundException e)
            {
                return null;
            }
            return result;
        }

        public BaseModel GetDataForExport()
        {
            if (DataForExport != null) return DataForExport;

            try
            {
                var directory = Environment.CurrentDirectory;
                var path = Path.Combine(directory, @"Task2.json");
                StreamReader reader = new StreamReader(path);
                var data = reader.ReadToEnd();
                var res = new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new DefaultNamingStrategy()
                    }
                };
                DataForExport = JsonConvert.DeserializeObject<BaseModel>(data, res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return DataForExport;
        }

        private void LoadData()
        {
            var store = new List<BaseModel>();
            try
            {
                var directory = Environment.CurrentDirectory;
                var path = Path.Combine(directory, @"Task3.json");
                StreamReader reader = new StreamReader(path);
                var data = reader.ReadToEnd();
                var res = new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new DefaultNamingStrategy()
                    }
                };
                store = JsonConvert.DeserializeObject<List<BaseModel>>(data, res);
                foreach (var s in store)
                {
                    Store.Add(s.Hotel.HotelID, s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}