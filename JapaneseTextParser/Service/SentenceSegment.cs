using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.Text.Json;
using JapaneseTextParseInfrastructure;
using JapaneseTextParserApplication.Service;
using JapaneseTextParserDomain.Model;

namespace JapaneseTextParser.Service
{

    public class SentenceSegment
    {
        public Task<List<WordPair>> CrawMaintext(string inputText)
        {
            var fetchFuragana = new FetchFuragana();
            var furaganaService = new Furagana(fetchFuragana);
            Console.WriteLine("test");

            var translationData = new TranslationData();
            var translationService = new FetchTranslation(translationData).AddTranslationToWord(furaganaService.ReturnFuragana(inputText));
            return translationService;
            //return furaganaService.ReturnFuragana(inputText);


        }
    }
}
