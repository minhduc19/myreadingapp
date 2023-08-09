using JapaneseTextParserDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapaneseTextParserApplication.IService
{
    public interface IFuragana
    {
        public List<WordPair> ReturnFuragana(string query);
    }
}
