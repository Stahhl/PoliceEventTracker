using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PoliceEventTracker.Domain.ApiModels;
using PoliceEventTracker.Domain.Other;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PoliceEventTracker.Data.Models
{
    internal class ApiAccess
    {
        public ApiAccess(ApplicationSettings settings)
        {
            applicationSettings = settings;
        }

        private ApplicationSettings applicationSettings;

        //Gets json response from api and converts it to C# 'Domain.ApiModels' classes
        [HttpGet]
        internal async Task<IEnumerable<ApiEvent>> ApiGet()
        {
            IEnumerable<ApiEvent> events = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(applicationSettings.ApiConnection);

                var responseTask = client.GetAsync("events");
                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    events = JsonConvert.DeserializeObject<IEnumerable<ApiEvent>>(json, new JsonSerializerSettings());
                }
            }
            return events;
        }
    }
}
