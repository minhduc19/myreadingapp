using JapaneseTextParseInfrastructure;
using JapaneseTextParserApplication.IService;
using JapaneseTextParserDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapaneseTextParserApplication.Service
{
    public class FetchTranslation : IFetchTranslation
    {
        private readonly TranslationData translationData;
        public FetchTranslation(TranslationData _translationData) {

            translationData = _translationData;
        }

        public Task<List<WordPair>> AddTranslationToWord(List<WordPair> listOfWord)
        {
            return translationData.TranslationFromJisho(listOfWord);
        }
    }
}
