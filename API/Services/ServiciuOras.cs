using System.Text.Json;
using System.Xml;
using API.Data;
using API.Entities;

namespace API.Services
{
    internal class ServiciuCity
    {
        // public async static Task<List<City>> PreiaDateCities()
        // {
        //     List<City> listaCities = new List<City>();
        //     XmlReaderSettings settings = new XmlReaderSettings()
        //     {
        //         Async = true,
        //     };
        //     XmlReader reader = XmlReader.Create("https://raw.githubusercontent.com/kristopolous/lists/master/cities.xml", settings);
        //     DateTime data = DateTime.UtcNow;
        //     City City = new City();

        //     while (await reader.ReadAsync())
        //     {
        //         if (reader.IsStartElement())
        //         {
        //             if (reader.Name == "ms:NAME")
        //             {
        //                 await reader.ReadAsync();
        //                 var numeCity = reader.Value;
        //                 City.Nume = numeCity;
        //                 City.Data = DateTime.SpecifyKind(data, DateTimeKind.Utc);
        //             }
        //             if (reader.Name == "gml:coordinates")
        //             {
        //                 await reader.ReadAsync();
        //                 var coordonate = reader.Value;
        //                 if(coordonate.Split(",").Length == 2)
        //                 {
        //                     var longitudine = double.Parse(coordonate.Split(",")[0], System.Globalization.CultureInfo.InvariantCulture);
        //                     var latitudine = double.Parse(coordonate.Split(",")[1], System.Globalization.CultureInfo.InvariantCulture);

        //                     City.Longitudine = longitudine;
        //                     City.Latitudine = latitudine;
        //                 }
                        
        //             }
        //         }
        //         if(!string.IsNullOrEmpty(City.Nume) && City.Latitudine !=0 && City.Longitudine != 0)
        //         {
        //             listaCities.Add(City);
        //             City = new City();

        //         }
        //     }

        //     return listaCities;
        // }

        // public async static Task<List<City>> PreiaDateCitiesDinCountry(string Country)
        // {
        //     List<City> listaCities = new List<City>();
        //     XmlReaderSettings settings = new XmlReaderSettings()
        //     {
        //         Async = true,
        //     };
        //     XmlReader reader = XmlReader.Create("https://raw.githubusercontent.com/kristopolous/lists/master/cities.xml", settings);
        //     DateTime data = DateTime.UtcNow;

        //     City City = new City();

        //     while (await reader.ReadAsync())
        //     {
        //         if (reader.IsStartElement())
        //         {
        //             if (reader.Name == "ms:NAME")
        //             {
        //                 await reader.ReadAsync();
        //                 var numeCity = reader.Value;
        //                 City.Nume = numeCity;
        //                 City.Data = DateTime.SpecifyKind(data, DateTimeKind.Utc);
        //             }
        //             if (reader.Name == "gml:coordinates")
        //             {
        //                 await reader.ReadAsync();
        //                 var coordonate = reader.Value;
        //                 if (coordonate.Split(",").Length == 2)
        //                 {
        //                     var longitudine = double.Parse(coordonate.Split(",")[0], System.Globalization.CultureInfo.InvariantCulture);
        //                     var latitudine = double.Parse(coordonate.Split(",")[1], System.Globalization.CultureInfo.InvariantCulture);

        //                     City.Longitudine = longitudine;
        //                     City.Latitudine = latitudine;
        //                 }

        //             }
        //         }
        //         if (!string.IsNullOrEmpty(City.Nume) && City.Latitudine != 0 && City.Longitudine != 0)
        //         {
        //             HttpClient client = new HttpClient();
        //             HttpContent inputContent = new StringContent("application/json");
        //             var apiUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={City.Latitudine}&lon={City.Longitudine}&appid=30630c295119a447a8fc7df85f51bdc4";
        //             HttpResponseMessage response = client.PostAsync(apiUrl, inputContent).Result;
        //             if (response.IsSuccessStatusCode)
        //             {
        //                 WeatherResponse weather = JsonSerializer.Deserialize<WeatherResponse>(response.Content.ReadAsStringAsync().Result);
        //                 if (weather != null)
        //                 {
        //                     if (weather.sys?.country != Country)
        //                     {
        //                         continue;
        //                     }
        //                     City.Country = new Country();
        //                     City.Country.CodCountry = weather.sys?.country;
        //                     //City.TemperaturaKelvin = (double)(weather.main?.temp);
        //                     //City.TemperaturaCelsius = City.TemperaturaKelvin - 273.15;
        //                     //City.TemperaturaFahrenheit = City.TemperaturaKelvin * 9 / 5 - 459.67;
        //                     var hoursUTC = weather.timezone / 3600;
        //                     //City.Ora = DateTime.Now.AddHours(hoursUTC - 2);

        //                     listaCities.Add(City);

        //                 }

        //             }
        //             else
        //             {
        //                 throw new Exception($"Eroare la apelarea url-ului de aflare vreme si timezone pentru Cityul {City.Nume}.");
        //             }

        //             City = new City();
        //         }
        //     }


        //     return listaCities;
        // }
    
    }
}
