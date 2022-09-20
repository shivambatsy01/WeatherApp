using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace WeatherApp.ViewModel.Helper
{
    public class AccuWeatherHelper
    {

        public const string BaseUrl = "http://dataservice.accuweather.com/";
        public const string APIKey = "rAg4wBSbAdoqlhw04xpmQuoO3jB1H0jI";

        public const string AutoCompleteEndpoint = "{0}locations/v1/cities/autocomplete?apikey={1}&q={2}";
        /*{0}=Baseurl,  {1}=apikey,  {2}=Text for autocomplete search (query text)*/

        public const string CurrentConditionEndpoint = "{0}currentconditions/v1/{1}?apikey={2}";
        /*{0}=Baseurl,  {1}=Citykey for city(extracted from autocomplete response),   {2}=apikey*/


        public static async Task<List<City>> GetCities(string LocationQuery)
        {
            var cityList = new List<City>();
            //make web request to get list of cities (converted fron json to c# list)
            string url =string.Format(AutoCompleteEndpoint,BaseUrl,APIKey,LocationQuery);
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json=await response.Content.ReadAsStringAsync();
                cityList=JsonConvert.DeserializeObject<List<City>>(json);

            }
            
            return cityList;
        }

        public static async Task<CurrentCondition> GetCurrentCondition(string CityKey)
        {
            CurrentCondition currentCondition= new CurrentCondition();
            string url =string.Format(CurrentConditionEndpoint,BaseUrl,CityKey,APIKey);
            using(HttpClient client=new HttpClient())

            {
                var response=await client.GetAsync(url);
                string json=await response.Content.ReadAsStringAsync();
                currentCondition=(JsonConvert.DeserializeObject<List<CurrentCondition>>(json)).FirstOrDefault();
                //as current condition json also returns list of current condition -> use first or default method
            }

            return currentCondition;
        }

    }
}
