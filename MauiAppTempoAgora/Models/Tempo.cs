namespace MauiAppTempoAgora.Models
{
    public class Tempo
    {
        // coordenadas
        public double? lon { get; set; }
        public double? lat { get; set; }


        // temperatura
        public double? temp_min { get; set; }
        public double? temp_max { get; set; }

        // visibilidade
        public int? visibility { get; set; }

        // velocidade vento
        public double? speed { get; set; }

        // descrição tempo
        public string? main { get; set; }
        public string? description { get; set; }

        // nascer e por do sol
        public string? sunrise { get; set; }
        public string? sunset { get; set; }

    }
}
