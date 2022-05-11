using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp;

public partial class BuecherView : ContentPage
{
	public BuecherView()
	{
		InitializeComponent();
        Task.Run(async () =>
        {
            BuecherList.ItemsSource = await GetBuecherAsync();
        }).Wait();
    }

    public async Task<List<Buch>> GetBuecherAsync()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
        HttpClient client = new HttpClient(handler);
        client.BaseAddress = new Uri("https://10.3.9.16:5001");

        List<Buch> answer = await client.GetFromJsonAsync<List<Buch>>("api/BuchAPI/");
        return answer;
    }
}