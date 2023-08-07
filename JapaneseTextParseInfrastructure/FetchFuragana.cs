using HtmlAgilityPack;
using JapaneseTextParserDomain.Model;
using System.Net;
using System.Text.Json;

namespace JapaneseTextParseInfrastructure
{
    public class FetchFuragana
    {
        public List<WordPair> FetchFuraganaFromJisho(string inputText)
        {
            string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string url = "https://jisho.org/search/" + inputText; // Replace this with the URL of the webpage you want to crawl

            List<WordPair> wordPairList = new List<WordPair>();
            // Create a WebClient to download the HTML content of the webpage
            using (WebClient client = new WebClient())
            {
                string htmlContent = client.DownloadString(url);

                // Use HtmlAgilityPack to parse the HTML content
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                // Get all elements with class name "japanese_word__furigana"
                //HtmlNodeCollection wordElements = htmlDocument.DocumentNode.SelectNodes("//span[contains(@class, 'japanese_word__text_wrapper')]");
                IEnumerable<HtmlNode> wordElements = htmlDocument.DocumentNode.Descendants("span")
                                            .Where(n => n.GetAttributeValue("class", "").Contains("japanese_word__text_wrapper"));
                IEnumerable<HtmlNode> furiganaElements = htmlDocument.DocumentNode.Descendants("span")
                                            .Where(n => n.GetAttributeValue("class", "").Contains("japanese_word__furigana_wrapper"));
                //HtmlNodeCollection furiganaElements = htmlDocument.DocumentNode.Descendants().Where(n => n.GetAttributeValue("class", "").Contains("japanese_word__furigana"));
                int count = 0;
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt")))
                {
                    if (furiganaElements != null)
                    {

                        for (int i = 0; i < wordElements.Count(); i++)
                        {
                            if (wordElements.ElementAt(i).InnerText.Trim() == furiganaElements.ElementAt(i).InnerText.Trim()) {
                                Console.WriteLine(wordElements.ElementAt(i).InnerText.Trim() + "the same as " + furiganaElements.ElementAt(i).InnerText.Trim());
                            }

                            wordPairList.Add(new WordPair(wordElements.ElementAt(i).InnerText.Trim(), furiganaElements.ElementAt(i).InnerText.Trim()));

                        }

                    }
                    
                    string jsonString = JsonSerializer.Serialize(wordPairList, new JsonSerializerOptions
                    {
                        // Set this option to ensure proper encoding of non-ASCII characters
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        WriteIndented = true // Optional: Format the JSON for readability
                    });
                    outputFile.WriteLine(jsonString);
                }

                return wordPairList;
            }

        }

    }

}
