using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RepairReach.Core.Model;

namespace RepairReach.Core.Service
{
    public class GeocodingService : IGeocodingService
    {
        public async Task<Location> GetLocation(string address)
        {
            var urlFriendlyAddress = address.Replace(" ", "+");
            //Need to replace the string and put "+" instead of space
            var geoUrl =
                String.Format(
                    "https://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=true",
                    urlFriendlyAddress);

            //&key=AIzaSyC8fi-20lWBxKpJJU-Zudkw6kbTHsRo-es
            HttpClient httpClient = new HttpClient();
            var results = await httpClient.GetAsync(geoUrl);
            var result = await results.Content.ReadAsStringAsync();
            var rootObject = JsonConvert.DeserializeObject<RootObject>(result);

            var item = rootObject.results.FirstOrDefault();
            return item.geometry.location;
        }
    }
}
