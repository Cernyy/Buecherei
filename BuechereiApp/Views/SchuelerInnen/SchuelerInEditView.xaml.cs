using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp;

public partial class SchuelerInEditView : ContentPage
{
	public static SchuelerIn schuelerin;
	public static int oldAusweisnummer;

	public HttpClientHandler handler = new HttpClientHandler();
	public HttpClient client;

	public SchuelerInEditView()
	{
		InitializeComponent();
		handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
		client = new HttpClient(handler);
		client.BaseAddress = new Uri("https://10.3.9.16:5001");

		this.BindingContext = schuelerin;
	}
	//Popup für Buchnummer
	async void EditVorname(object sender, EventArgs args)
	{
		try
		{
			schuelerin.Vorname = await DisplayPromptAsync("Vorname", "Geben Sie einen neuen Vorname ein:", initialValue: schuelerin.Vorname, maxLength: 50);
			LabelVorname.Text = schuelerin.Vorname; ;
		}
		catch (Exception e)
		{
			await DisplayAlert("ERROR", "Etwas ist schiefgelaufen.\n" + e.Message, "OK");
		}
	}
	//Popup für Ausweisnummer
	async void EditNachname(object sender, EventArgs args)
	{
		try
		{
			schuelerin.Nachname = await DisplayPromptAsync("Nachname", "Geben Sie einen neuen Nachname ein:", initialValue: schuelerin.Nachname, maxLength: 50);
			LabelNachname.Text = schuelerin.Nachname;
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
			schuelerin.Ausweisnummer = int.Parse(await DisplayPromptAsync("Ausweisnummer", "Geben Sie eine neue Ausweisnummer ein:", initialValue: schuelerin.Ausweisnummer.ToString()));
			LabelAusweisnummer.Text = schuelerin.Ausweisnummer.ToString();
		}
		catch (Exception e)
		{
			await DisplayAlert("ERROR", "Etwas ist schiefgelaufen.\n" + e.Message, "OK");
		}

	}
	//ButtonEvent Zurück zur Liste
	void Abbrechen(object sender, EventArgs args)
	{
		App.Current.MainPage = new NavigationPage(new SchuelerInView());
	}
	//Überladung für Abbrechen
	void Abbrechen()
	{
		var view = new SchuelerInView();
		view.RefreshSchuelerIn();
		App.Current.MainPage = new NavigationPage(view);
	}
	//Änderungen speichern
	async void SaveChanges(object sender, EventArgs args)
	{
		await client.PutAsJsonAsync("api/SchuelerInAPI/" + oldAusweisnummer, schuelerin);
		Abbrechen();
	}
}