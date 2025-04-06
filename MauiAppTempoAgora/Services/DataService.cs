using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {

        public static async Task <Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;
            string chave = "a2cfb91d7e9563674459c8d76e3b372f";

            // consulta
            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage resp = await client.GetAsync(url);
                    if (resp.IsSuccessStatusCode)
                {
                    string jason= await resp.Content.ReadAsStringAsync();
                    var rascunho = JObject.Parse(jason);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()
                    {
                        // coordenadas
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],

                        // descrição tempo
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],

                        // temperatura
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],

                        // velocidade vento
                        speed = (double)rascunho["wind"]["speed"],

                        // visibilidade
                        visibility = (int)rascunho["visibility"],

                        // nascer e por do sol
                        sunrise = sunrise.ToString(),
                        sunset = sunset.ToString(),

                    };// fecha objeto do tempo
                }// fecha if
            }// fecha laço
            return t;
        }
    }
}
