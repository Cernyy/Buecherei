using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp;

public partial class AusleiheEditView : ContentPage
{
	public static Ausleihe ausleihe;

	public HttpClientHandler handler = new HttpClientHandler();
	public HttpClient client;

	public AusleiheEditView()
	{
		InitializeComponent();
		handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
		client = new HttpClient(handler);
		client.BaseAddress = new Uri("https://10.3.9.16:5001");

		this.BindingContext = ausleihe;
	}
	//Popup für Buchnummer
	async void EditBuchnummer(object sender, EventArgs args)
	{
		try
		{
			ausleihe.Buchnummer = await DisplayPromptAsync("Buchnummer", "Geben Sie eine neue Buchnummer ein:", initialValue: ausleihe.Buchnummer, maxLength: 6, keyboard: Keyboard.Numeric);
			LabelBuchnummer.Text = ausleihe.Buchnummer; ;
		}
		catch (Exception e)
		{
			await DisplayAlert("ERROR", "Etwas ist schiefgelaufen.\n" + e.Message, "OK");
		}
	}
	//Popup für Ausweisnummer
	async void EditAusweisnummer(object sender, EventArgs args)
	{
		try
		{
			ausleihe.Ausweisnummer = int.Parse( await DisplayPromptAsync("Ausweisnummer", "Geben Sie eine neue Ausweisnummer ein:", initialValue: ausleihe.Ausweisnummer.ToString()));
			LabelBuchnummer.Text = ausleihe.Ausweisnummer.ToString();
		}
		catch (Exception e)
		{
			await DisplayAlert("ERROR", "Etwas ist schiefgelaufen.\n" + e.Message, "OK");
		}
		
	}
	//Popup für Ausleihdatum
	async void EditAusleihdatum(object sender, EventArgs args)
	{
		try
		{
			if (ausleihe.Retourdatum != null)
			{
				ausleihe.Ausleihdatum = DateTime.Parse(await DisplayPromptAsync("Ausleihdatum", "Geben Sie eine neues Ausleihdatum ein:", initialValue: ausleihe.Ausleihdatum.ToString()));
			}
			else
			{
				ausleihe.Ausleihdatum = DateTime.Parse(await DisplayPromptAsync("Ausleihdatum", "Geben Sie eine neues Retourdatum ein:", initialValue: DateTime.Now.ToString()));

			}
			LabelBuchnummer.Text = ausleihe.Ausleihdatum.ToString();
		}
		catch(Exception e)
        {
			await DisplayAlert("ERROR", "Etwas ist schiefgelaufen.\n" + e.Message, "OK");
		}
	}
	//Popup für Retourdatum
	async void EditRetourdatum(object sender, EventArgs args)
	{
		try
		{
            if (ausleihe.Retourdatum != null)
            {
				ausleihe.Retourdatum = DateTime.Parse(await DisplayPromptAsync("Retourdatum", "Geben Sie eine neues Retourdatum ein:", initialValue: ausleihe.Retourdatum.ToString()));
            }
            else
            {
				ausleihe.Retourdatum = DateTime.Parse(await DisplayPromptAsync("Retourdatum", "Geben Sie eine neues Retourdatum ein:", initialValue: DateTime.Now.ToString()));
				
			}
			LabelBuchnummer.Text = ausleihe.Retourdatum.ToString();
			
		}catch(Exception e)
        {
			await DisplayAlert("ERROR", "Etwas ist schiefgelaufen.\n" + e.Message, "OK");
        }
	}
	//ButtonEvent Zurück zur Liste
	void Abbrechen(object sender, EventArgs args)
	{
		App.Current.MainPage = new NavigationPage(new AusleiheView());
	}
	//Überladung für Abbrechen
	void Abbrechen()
	{
		var view = new AusleiheView();
		view.RefreshAusleihe();
		App.Current.MainPage = new NavigationPage(view);
	}
	//Änderungen speichern
	async void SaveChanges(object sender, EventArgs args)
	{
		await client.PutAsJsonAsync("api/AusleiheAPI/" + ausleihe.Id, ausleihe);
		Abbrechen();
	}
}