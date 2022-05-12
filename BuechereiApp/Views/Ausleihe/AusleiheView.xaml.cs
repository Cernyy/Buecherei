using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp;

public partial class AusleiheView : ContentPage
{
    public HttpClientHandler handler = new HttpClientHandler();
    public HttpClient client;
    
    public AusleiheView()
	{
		InitializeComponent();
        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
        client = new HttpClient(handler);
        client.BaseAddress = new Uri("https://10.3.9.16:5001");
        Task.Run(async () =>{
            AusleihenList.ItemsSource = await GetAusleihenAsync();
        }).Wait();
        
    }

    public async Task<List<Ausleihe>> GetAusleihenAsync()
    {
        List<Ausleihe> ausleihen = await client.GetFromJsonAsync<List<Ausleihe>>("api/AusleiheAPI/");
        return ausleihen;
    }

    //ButtonEvent um einen Ausleihe zu l�schen
    async void AusleiheLoeschen(object sender, EventArgs args)
    {
        if ((Ausleihe)AusleihenList.SelectedItem != null)
        {
            if (!await DisplayAlert("Best�tigung", "M�chten Sie dieses Element sicher l�schen?", "Nein", "Ja"))
            {
                await client.DeleteAsync("api/AusleiheAPI/" + ((Ausleihe)AusleihenList.SelectedItem).Id);
                AusleihenList.ItemsSource = await GetAusleihenAsync();
                RefreshAusleihe();
            }
        }
    }
    //ButtonEvent um die Liste zu Refreshen
    void RefreshAusleihe(object sender, EventArgs args)
    {
         RefreshAusleihe();
    }
    //�berladene Methode damit man die Methode auch im Backend aufrufen kann
    public async void RefreshAusleihe()
    {
        AusleihenList.ItemsSource = await GetAusleihenAsync();        
    }
    //ButtonEvent um zur Bearbeiten bzw Detail Ansicht zu springen
    void RedirectToEdit(object sender, EventArgs args)
    {
        EditView.ausleihe = client.GetFromJsonAsync<Ausleihe>("api/AusleiheAPI/" + ((Ausleihe)AusleihenList.SelectedItem).Id).Result;
        App.Current.MainPage = new NavigationPage(new EditView());
    }
}