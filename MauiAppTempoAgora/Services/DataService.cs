﻿using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;
            string chave = "a2cfb91d7e9563674459c8d76e3b372f";


            // descição em protuguês
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&lang=pt_br&appid={chave}";


            try
            {
                using HttpClient client = new();

                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.StatusCode == HttpStatusCode.NotFound)
                {
                    return null; // cidade inválida
                }

                if (!resp.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Erro de requisição: {resp.StatusCode}");
                }

                string jason = await resp.Content.ReadAsStringAsync();
                var rascunho = JObject.Parse(jason);

                DateTime time = new();
                DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                t = new()
                {
                    lat = (double)rascunho["coord"]["lat"],
                    lon = (double)rascunho["coord"]["lon"],
                    description = (string)rascunho["weather"][0]["description"],
                    main = (string)rascunho["weather"][0]["main"],
                    temp_min = (double)rascunho["main"]["temp_min"],
                    temp_max = (double)rascunho["main"]["temp_max"],
                    speed = (double)rascunho["wind"]["speed"],
                    visibility = (int)rascunho["visibility"],
                    sunrise = sunrise.ToString(),
                    sunset = sunset.ToString(),
                };

                return t;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Sem conexão com a internet.Verifique sua rede.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado: " + ex.Message);
            }
        }
    }
}
