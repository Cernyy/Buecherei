using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp;


public partial class SchuelerInnenView : ContentPage
{
    public SchuelerInnenView()
	{
		InitializeComponent();
        Task.Run(async () =>
        {
            SchuelerInnenList.ItemsSource = await GetSchuelerInnenAsync();
        }).Wait();
    }

    public async Task<List<SchuelerIn>> GetSchuelerInnenAsync()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
        HttpClient client = new HttpClient(handler);
        client.BaseAddress = new Uri("https://10.3.9.16:5001");

        List<SchuelerIn> answer = await client.GetFromJsonAsync<List<SchuelerIn>>("api/SchuelerInAPI/");
        return answer;
    }
}