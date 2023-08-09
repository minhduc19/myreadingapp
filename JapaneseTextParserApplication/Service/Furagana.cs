using HtmlAgilityPack;
using JapaneseTextParserApplication.IService;
using JapaneseTextParserDomain.Model;
using System.Net;
using System.Text.Json;
using JapaneseTextParseInfrastructure;

namespace JapaneseTextParserApplication.Service
{
    public class Furagana : IFuragana
    {
        private readonly FetchFuragana furaganaData;
        public Furagana(FetchFuragana _furaganaData) {
            furaganaData = _furaganaData;
        }

        public List<WordPair> ReturnFuragana(string query)
        {
            return furaganaData.FetchFuraganaFromJisho(query);
        }
    }
}