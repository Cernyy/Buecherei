using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp;

public partial class AusleiheView : ContentPage
{
	public AusleiheView()
	{
		InitializeComponent();
        Task.Run(async () =>
        {
            AusleihenList.ItemsSource = await GetAusleihenAsync();
        }).Wait();
    }

    public async Task<List<Ausleihe>> GetAusleihenAsync()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
        HttpClient client = new HttpClient(handler);
        client.BaseAddress = new Uri("https://10.3.9.16:5001");

        List<Ausleihe> answer = await client.GetFromJsonAsync<List<Ausleihe>>("api/AusleiheAPI/");
        return answer;
    }
}