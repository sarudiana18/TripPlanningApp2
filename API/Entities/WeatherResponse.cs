namespace API.Entities
{
    public class WeatherResponse
    {
        public Main main { get; set; }
        public double timezone { get; set; }
        public Sys sys { get; set; }
    }
    public class Main
    {
        public double temp { get; set; }

    }
    public class Sys
    {
        public string country { get; set; }

    }
}
