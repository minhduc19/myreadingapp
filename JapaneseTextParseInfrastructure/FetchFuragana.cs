using HtmlAgilityPack;
using JapaneseTextParserDomain.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            //string url = "https://jisho.org/search/" + inputText; // Replace this with the URL of the webpage you want to crawl
            //IWebDriver driver = new ChromeDriver();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");
            chromeOptions.AddArguments("--no-sandbox");
            chromeOptions.AddArguments("--disable-dev-shm-usage");
            chromeOptions.AddArguments("--disable-features=NetworkService");
            chromeOptions.AddArguments("--window-size=1920x1080");
            chromeOptions.AddArguments("--disable-features=VizDisplayCompositor");
            //chromeOptions.AddArguments("--remote-debugging-port=9222");
            IWebDriver driver = new ChromeDriver(chromeOptions);
            // Navigate to the web page
            driver.Navigate().GoToUrl("https://jisho.org/");
            // Find the input element and enter text
            IWebElement inputElement = driver.FindElement(By.Id("keyword"));
            inputElement.SendKeys(inputText);
            // Find the submit button and click it
            IWebElement submitButton = driver.FindElement(By.ClassName("submit"));
            submitButton.Click();
            // Get the page source as a string
            string pageSource = driver.PageSource;

            List<WordPair> wordPairList = new List<WordPair>();
            // Create a WebClient to download the HTML content of the webpage
        

                // Use HtmlAgilityPack to parse the HTML content
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(pageSource);

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
            // Close the browser
            driver.Close();
            driver.Quit();
            return wordPairList;
            }


    }

}
