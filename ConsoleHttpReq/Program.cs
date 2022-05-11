// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;

HttpClientHandler handler = new HttpClientHandler();
handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
HttpClient client = new HttpClient(handler);
client.BaseAddress = new Uri("https://10.3.9.16:5001");

try
{
    var s = new SchuelerIn() { Ausweisnummer = 133, Nachname = "juliane", Vorname = "welt" };
    var request = client.PostAsJsonAsync("api/SchuelerInAPI/133", s);

    var responseBody = await request.Result.Content.ReadAsStringAsync();
    Console.WriteLine(responseBody);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

public class SchuelerIn
{
    public int Ausweisnummer { get; set; }
    public string Vorname { get; set; }
    public string Nachname { get; set; }
}