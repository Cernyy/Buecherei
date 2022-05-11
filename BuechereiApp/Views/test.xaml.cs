using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp;

public partial class test : ContentPage
{
	public test()
	{
		InitializeComponent();
        Task.Run(async () =>
        {
            var test = await GetAusleihenAsync();
            AusleihenList.ItemsSource = test;
        }).Wait();
    }

    public async Task<List<Ausleihe>> GetAusleihenAsync()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
        HttpClient client = new HttpClient(handler);
        client.BaseAddress = new Uri("https://10.3.9.16:5001");

        List<Ausleihe> ausleihen = await client.GetFromJsonAsync<List<Ausleihe>>("api/AusleiheAPI/");
        foreach (Ausleihe item in ausleihen)
        {
            item.SchuelerIn = await client.GetFromJsonAsync<SchuelerIn>("api/SchuelerInAPI/" + item.Ausweisnummer);
            item.Buch = await client.GetFromJsonAsync<Buch>("api/BuchAPI/" + item.Buchnummer);
        }
        return ausleihen;
    }

    //ButtonEvents
    async void AusleiheLoeschen(object sender, EventArgs args)
    {
        bool abfrage = await DisplayAlert("Frage", "Möchten Sie dieses Element löschen?", "Ja", "Nein");
        if (abfrage)
        {
            //delete
        }
    }
}