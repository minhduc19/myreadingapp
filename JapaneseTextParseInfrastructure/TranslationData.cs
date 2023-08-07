using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JapaneseTextParserDomain.Model;
using Microsoft.Extensions.DependencyInjection;

namespace JapaneseTextParseInfrastructure
{
    public class TranslationData
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public TranslationData(IHttpClientFactory httpClientFactory)
       // {
         //   _httpClientFactory = httpClientFactory;
        //}
        public async Task<List<WordPair>> TranslationFromJisho(List<WordPair> ListOfWordAndFuragana) {
            string url = "https://jisho.org/api/v1/search/words?keyword=";
             //Create a new Service Collection
            var services = new ServiceCollection();

             //Add HttpClient to the Service Collection
            services.AddHttpClient();

             //Build the Service Provider
            var serviceProvider = services.BuildServiceProvider();

             //Use the Service Provider to get an instance of HttpClientFactory
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            foreach (WordPair wordPair in ListOfWordAndFuragana)
            {
                url = url + wordPair.word;
                try
                {
                    // Create a named HttpClient using HttpClientFactory
                    var httpClient = httpClientFactory.CreateClient();
                    // Set the base address (optional)
                    httpClient.BaseAddress = new Uri(url);
                    // Send the HTTP GET request and get the response
                    HttpResponseMessage response = await httpClient.GetAsync("");
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Translation translationContentInJson = JsonSerializer.Deserialize<Translation>(responseContent);
                        wordPair.translation = translationContentInJson;
                        url = "https://jisho.org/api/v1/search/words?keyword=";
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return ListOfWordAndFuragana;



        }
    }
}
