using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);
                    if (t != null)

                    {                           // coordenadas
                        string dados_previsao = $"Latitude: {t.lat} \n" +
                                                $"Longitude: {t.lon} \n" +

                                                // temperatura
                                                $"Temperatura Mínima: {t.temp_min} \n" +
                                                $"Temperatura Máxima: {t.temp_max} \n" +

                                                // visibilidade
                                                $"Visibilidade: {t.visibility} \n" +

                                                // velocidade vento
                                                $"Velocidade do Vento: {t.speed} \n" +

                                                // descrição tempo
                                                $"Main: {t.main} \n" +
                                                $"Descrição: {t.description} \n" +

                                                // nascer e por do sol
                                                $"Nascer do Sol: {t.sunrise} \n" +
                                                $"Por do Sol: {t.sunset} \n";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        await DisplayAlert("Cidade não encontrada", "Digite um nome de cidade válido.", "OK");
                        lbl_res.Text = "";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a Cidade.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Sem conexão com a internet", ex.Message, "OK");
            }
        }
    }
}