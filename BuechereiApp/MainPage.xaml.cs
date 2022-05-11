using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            Task.Run(async () =>
            {
                Select.ItemsSource = await GetAusleihenAsync();
            }).Wait();
            
            
            
            

        }

        public async Task<IEnumerable<Ausleihe>> GetAusleihenAsync()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
            HttpClient client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://10.3.9.16:5001");

            IEnumerable<Ausleihe> answer = await client.GetFromJsonAsync<IEnumerable<Ausleihe>>("api/AusleihAPI/");
            return answer;
        }
    }
}