using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp;

public partial class SchuelerInView : ContentPage
{
    public HttpClientHandler handler = new HttpClientHandler();
    public HttpClient client;
    
    public SchuelerInView()
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

    //ButtonEvent um einen SchuelerIn zu l�schen
    async void SchuelerInLoeschen(object sender, EventArgs args)
    {
        if ((SchuelerIn)SchuelerInnenList.SelectedItem != null)
        {
            if (!await DisplayAlert("Best�tigung", "M�chten Sie dieses Element sicher l�schen?", "Nein", "Ja"))
            {
                await client.DeleteAsync("api/SchuelerInAPI/" + ((SchuelerIn)SchuelerInnenList.SelectedItem).Ausweisnummer);
                SchuelerInnenList.ItemsSource = await GeSchuelerInnenAsync();
                RefreshSchuelerIn();
            }
        }
        else
        {
            await DisplayAlert("Information", "Sie haben kein Element ausgew�hlt!\nDr�cken Sie auf einen Eintrag um ihn auszuw�hlen", "OK");
        }
    }
    //ButtonEvent um die Liste zu Refreshen
    void RefreshSchuelerIn(object sender, EventArgs args)
    {
        RefreshSchuelerIn();
    }
    //�berladene Methode damit man die Methode auch im Backend aufrufen kann
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
            await DisplayAlert("Information", "Sie haben kein Element ausgew�hlt!\nDr�cken Sie auf einen Eintrag um ihn auszuw�hlen", "OK");
        }
    }
    //ButtonEvent um zur Bearbeiten bzw Detail Ansicht zu springen
    void RedirectToCreate(object sender, EventArgs args)
    {
        SchuelerInCreateView.schuelerin = new SchuelerIn();
        App.Current.MainPage = new NavigationPage(new SchuelerInCreateView());
    }
}