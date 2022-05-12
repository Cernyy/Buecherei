using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp;

public partial class EditView : ContentPage
{
	public static Ausleihe ausleihe;

	public HttpClientHandler handler = new HttpClientHandler();
	public HttpClient client;

	public EditView()
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
		ausleihe.Buchnummer = await DisplayPromptAsync("Buchnummer", "Geben Sie eine neue Buchnummer ein:", initialValue: ausleihe.Buchnummer, maxLength: 6, keyboard: Keyboard.Numeric);
		LabelBuchnummer.Text = ausleihe.Buchnummer;
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
		var test = client.GetFromJsonAsync<Ausleihe>("api/AusleiheAPI/" + ausleihe.Id).Result;
		Abbrechen();
	}
}