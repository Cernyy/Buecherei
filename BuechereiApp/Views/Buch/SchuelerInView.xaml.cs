using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp;

public partial class BuchView : ContentPage
{
    public HttpClientHandler handler = new HttpClientHandler();
    public HttpClient client;
    
    public BuchView()
	{
		InitializeComponent();
        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
        client = new HttpClient(handler);
        client.BaseAddress = new Uri("https://10.3.9.16:5001");
        Task.Run(async () =>{
            SchuelerInnenList.ItemsSource = await GeSchuelerInnenAsync();
        }).Wait();
        
    }

    public async Task<List<SchuelerIn>> GeSchuelerInnenAsync()
    {
        List<SchuelerIn> ausleihen = await client.GetFromJsonAsync<List<SchuelerIn>>("api/SchuelerInAPI/");
        return ausleihen;
    }

    //ButtonEvent um einen SchuelerIn zu löschen
    async void BuchLoeschen(object sender, EventArgs args)
    {
        if ((SchuelerIn)BuchList.SelectedItem != null)
        {
            if (!await DisplayAlert("Bestätigung", "Möchten Sie dieses Element sicher löschen?", "Nein", "Ja"))
            {
                await client.DeleteAsync("api/SchuelerInAPI/" + ((Buch)BuchList.SelectedItem).Buchnummer);
                BuchList.ItemsSource = await GeBuecherAsync();
                RefreshSchuelerIn();
            }
        }
        else
        {
            await DisplayAlert("Information", "Sie haben kein Element ausgewählt!\nDrücken Sie auf einen Eintrag um ihn auszuwählen", "OK");
        }
    }
    //ButtonEvent um die Liste zu Refreshen
    void RefreshSchuelerIn(object sender, EventArgs args)
    {
        RefreshSchuelerIn();
    }
    //Überladene Methode damit man die Methode auch im Backend aufrufen kann
    public async void RefreshSchuelerIn()
    {
        SchuelerInnenList.ItemsSource = await GeSchuelerInnenAsync();        
      }
    //ButtonEvent um zur Bearbeiten bzw Detail Ansicht zu springen
    async void RedirectToEdit(object sender, EventArgs args)
    {
        if (SchuelerInnenList.SelectedItem != null)
        {
            
            SchuelerInEditView.schuelerin = client.GetFromJsonAsync<SchuelerIn>("api/SchuelerInAPI/" + ((SchuelerIn)SchuelerInnenList.SelectedItem).Ausweisnummer).Result;
            SchuelerInEditView.oldAusweisnummer = SchuelerInEditView.schuelerin.Ausweisnummer;
            App.Current.MainPage = new NavigationPage(new SchuelerInEditView());
        }
        else
        {
            await DisplayAlert("Information", "Sie haben kein Element ausgewählt!\nDrücken Sie auf einen Eintrag um ihn auszuwählen", "OK");
        }
    }
    //ButtonEvent um zur Bearbeiten bzw Detail Ansicht zu springen
    void RedirectToCreate(object sender, EventArgs args)
    {
        SchuelerInCreateView.schuelerin = new SchuelerIn();
        App.Current.MainPage = new NavigationPage(new SchuelerInCreateView());
    }
}