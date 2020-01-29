using System;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        RestService _restService;

        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }

        async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
                {
                    WeatherDataCurrent weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.EndpointCurrent));

                    // BindingContext = cm;
                    this.temperatura.Text = weatherData.data[0].temp.ToString();
                    this.humedad.Text = weatherData.data[0].rh.ToString();
                    this.rayos.Text = weatherData.data[0].uv.ToString();
                    this.descripcion.Text = weatherData.data[0].weather.description;
                }
            }
            catch(Exception ex)
            {
                string v = ex.Message;
            }
        }



        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?city={_cityEntry.Text}";
            requestUri += $"&key={Constants.Key}&units=M";
            return requestUri;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                WeatherDataDaily weatherData = await _restService.GetWeatherDataDayly(GenerateRequestUri(Constants.EndpointDayly));

                await this.Navigation.PushAsync(new DailyPage(weatherData.data));
            }
            catch
            { }
        }
    }
}
