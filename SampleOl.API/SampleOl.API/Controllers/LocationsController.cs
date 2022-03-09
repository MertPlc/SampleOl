using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SampleOl.API.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleOl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private string fileName;
        
        public LocationsController(IConfiguration configuration)
        {
            Configuration = configuration;
            fileName = Configuration.GetSection("FileName").Value;
        }

        [HttpGet]
        public List<Location> GetLocations()
        {
            if (System.IO.File.Exists(fileName))
            {
                string json = System.IO.File.ReadAllText(fileName);
                var jsonObject = JsonConvert.DeserializeObject<List<Location>>(json);   // JSON DESERIALIZATION

                return jsonObject;
            }

            return null;
        }

        [HttpPost]
        public IActionResult SaveLocation(Location location)
        {
            if (!System.IO.File.Exists(fileName))
            {
                using (FileStream fs = System.IO.File.Create(fileName));
            }

            string json = System.IO.File.ReadAllText(fileName);  //DİSKTEN OKUMA

            var jsonObject = JsonConvert.DeserializeObject<List<Location>>(json);

            if (jsonObject == null)
            {
                jsonObject = new List<Location>();
            }

            jsonObject.Add(location);

            string jsonSerialize = System.Text.Json.JsonSerializer.Serialize(jsonObject);
            System.IO.File.WriteAllText(fileName, jsonSerialize);

            return Ok();
        }
    }
}
