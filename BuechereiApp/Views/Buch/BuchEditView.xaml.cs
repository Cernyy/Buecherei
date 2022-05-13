using BuechereiApp.Models;
using System.Net.Http.Json;

namespace BuechereiApp;

public partial class BuchEditView : ContentPage
{
	public static Buch buch;
	public static int oldBuchnummer;

	public HttpClientHandler handler = new HttpClientHandler();
	public HttpClient client;

	public BuchEditView()
	{
		InitializeComponent();
		handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
		client = new HttpClient(handler);
		client.BaseAddress = new Uri("https://10.3.9.16:5001");

		this.BindingContext = buch;
	}
	//Popup f�r Sachgebiet
	async void EditSachgebiet(object sender, EventArgs args)
	{
		try
		{
			buch.Sachgebiet = await DisplayPromptAsync("Sachgebiet", "Geben Sie ein neues Sachgebiet ein:", initialValue: buch.Sachgebiet, maxLength: 50);
			LabelSachgebiet.Text = buch.Sachgebiet; ;
		}
		catch (Exception e)
		{
			await DisplayAlert("ERROR", "Etwas ist schiefgelaufen.\n" + e.Message, "OK");
		}
	}
	//Popup f�r Titel
	async void Titel(object sender, EventArgs args)
	{
		try
		{
			buch.Titel = await DisplayPromptAsync("Titel", "Geben Sie einen neuen Titel ein:", initialValue: buch.Titel, maxLength: 50);
			LabelTitel.Text = buch.Titel; ;
		}
		catch (Exception e)
		{
			await DisplayAlert("ERROR", "Etwas ist schiefgelaufen.\n" + e.Message, "OK");
		}
	}
	//Popup f�r Autor
	async void Autor(object sender, EventArgs args)
	{
		try
		{
			buch.Autor = await DisplayPromptAsync("Autor", "Geben Sie einen neuen Autor ein:", initialValue: buch.Autor, maxLength: 50);
			LabelAutor.Text = buch.Autor; ;
		}
		catch (Exception e)
		{
			await DisplayAlert("ERROR", "Etwas ist schiefgelaufen.\n" + e.Message, "OK");
		}
	}
	//Popup f�r Ort
	async void Ort(object sender, EventArgs args)
	{
		try
		{
			buch.Ort = await DisplayPromptAsync("Ort", "Geben Sie einen neuen Ort ein:", initialValue: buch.Ort);
			LabelOrt.Text = buch.Ort;
		}
		catch (Exception e)
		{
			await DisplayAlert("ERROR", "Etwas ist schiefgelaufen.\n" + e.Message, "OK");
		}
	}
	//Popup f�r Buchnummer
	async void EditBuchnummer(object sender, EventArgs args)
	{
		try
		{
			buch.Buchnummer = await DisplayPromptAsync("Buchnummer", "Geben Sie eine neue Buchnummer ein:", initialValue: buch.Buchnummer.ToString());
			LabelBuchnummer.Text = buch.Buchnummer.ToString();
		}
		catch (Exception e)
		{
			await DisplayAlert("ERROR", "Etwas ist schiefgelaufen.\n" + e.Message, "OK");
		}

	}


	//ButtonEvent Zur�ck zur Liste
	void Abbrechen(object sender, EventArgs args)
	{
		App.Current.MainPage = new NavigationPage(new BuchView());
	}
	//�berladung f�r Abbrechen
	void Abbrechen()
	{
		var view = new BuchView();
		view.RefreshBuch();
		App.Current.MainPage = new NavigationPage(view);
	}
	//�nderungen speichern
	async void SaveChanges(object sender, EventArgs args)
	{
		await client.PutAsJsonAsync("api/BuchAPI/" + oldBuchnummer, buch);
		Abbrechen();
	}
}