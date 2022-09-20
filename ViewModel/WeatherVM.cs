using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModel.Helper;
using System.ComponentModel;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using System.Collections.ObjectModel;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {

        private string cityNameQuery;
        private CurrentCondition currentCondition;
        public ObservableCollection<City> CityList { get; set; }  //need to be a property
        private City selectedCity;
        public QuerySearchCommand searchCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged; //object to use iNotifyPropertyChanged Event handlers
        private void OnPropertyChange(string PropertyName)
        {
            //if property changes event exist -> this will invoke PropertyName Property in Xamls
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }


        public WeatherVM()  //constructor to start the xaml
        {
            selectedCity = new City()
            {
                LocalizedName = "New Delhi"
            };
            currentCondition = new CurrentCondition()
            {
                WeatherText = "Sunny",
                Temperature = new Temperature()
                {
                    Metric = new Units
                    {
                        Value = 31
                    }
                }
            };
            searchCommand = new QuerySearchCommand(this);
            CityList=new ObservableCollection<City>();

            City c1 = new City()
            {
                LocalizedName = "kanpur"
            };
            CityList.Add(c1);
            City c2 = new City()
            {
                LocalizedName = "Lucknow"
            };
            CityList.Add(c2);
            City c3 = new City()
            {
                LocalizedName = "Agra"
            };
            CityList.Add(c3);

        }

        public string CityNameQuery  //query to type in searchtext bar -> object to bind to search query
        {
            get { return cityNameQuery; }
            set
            {
                cityNameQuery = value;
                OnPropertyChange("CityNameQuery");
            }
        }

        public CurrentCondition MyCurrentCondition  //object to bing to currentconditions
        {
            get { return currentCondition; }
            set
            {
                currentCondition = value;
                OnPropertyChange("MyCurrentCondition");
            }
        }
        
        public City SelectedCity  //object to bind to selected city (from listview)
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChange("SelectedCity");
            }
        }

        //the below function call the http fuctions from helper and update the private declared objects of weatherVM
        //these functions shall be called by commands in ViewModel.Commands
        //Commands will be binded to XAML buttons etc
        public async void MakeQuery()
        {
            var cityList = await AccuWeatherHelper.GetCities(cityNameQuery);
            //Now this cityList need to be converted to observable collection that can be binded to Xaml
            CityList.Clear();
            foreach (City city in cityList)
                CityList.Add(city);
        }

        /*        public List<City> CityList 
                { 
                    get{ return cityList;}
                    set{ CityList = value;}
                }*/



    }
}
