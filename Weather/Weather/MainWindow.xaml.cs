using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace Weather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string urlWeather = "http://api.openweathermap.org/data/2.5/forecast?q=*&appid=318d86e610dfb76c01702642e1ff63e1";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnterSearch(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(searchByCityTX.Text) == false)
            {
                if (urlWeather.Contains("*"))
                {
                    int IndexFirst = urlWeather.IndexOf("*");
                    urlWeather = urlWeather.Remove(IndexFirst, "*".Length).Insert(IndexFirst, searchByCityTX.Text);
                }
                WebClient webClient = new WebClient();

                List<InfoWeather> weathers = new List<InfoWeather>();

                weathers.Add(JsonConvert.DeserializeObject<InfoWeather>(webClient.DownloadString(urlWeather)));

                List<Main> mains = new List<Main>();
                List<DescripWeather> descripWeathers = new List<DescripWeather>();
                List<DateTime> dateTimes = new List<DateTime>();
                foreach (var weather in weathers)
                {
                    mains.Add(weather.MainWheathers);
                    descripWeathers.Add(weather.Descrip);
                    dateTimes.Add(weather.DateTimeWeather);
                }

                WeatherDataGrid weatherDataGrid = new WeatherDataGrid();
                weatherDataGrid.Temp = mains[0].Temp;
                weatherDataGrid.Main = descripWeathers[0].Main;
                weatherDataGrid.Description = descripWeathers[0].Description;
                weatherDataGrid.WheaterDT = dateTimes[0];

                List<WeatherDataGrid> weatherDataGrids = new List<WeatherDataGrid>();
                weatherDataGrids.Add(weatherDataGrid);

                tableWeather.ItemsSource = weatherDataGrids;

            }
            else
            {
                MessageBox.Show("Вы ввели пустую строку!");
            }
        }
    }
}
