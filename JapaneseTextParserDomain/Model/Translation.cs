using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapaneseTextParserDomain.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attribution
    {
        public bool jmdict { get; set; }
        public bool jmnedict { get; set; }
        public object dbpedia { get; set; }
    }

    public class Datum
    {
        public string slug { get; set; }
        public bool is_common { get; set; }
        public List<string> tags { get; set; }
        public List<string> jlpt { get; set; }
        public List<Japanese> japanese { get; set; }
        public List<Sense> senses { get; set; }
        public Attribution attribution { get; set; }
    }

    public class Japanese
    {
        public string word { get; set; }
        public string reading { get; set; }
    }

    public class Link
    {
        public string text { get; set; }
        public string url { get; set; }
    }

    public class Meta
    {
        public int status { get; set; }
    }

    public class Translation
    {
        public Meta meta { get; set; }
        public List<Datum> data { get; set; }
    }

    public class Sense
    {
        public List<string> english_definitions { get; set; }
        public List<string> parts_of_speech { get; set; }
        public List<Link> links { get; set; }
        public List<string> tags { get; set; }
        public List<object> restrictions { get; set; }
        public List<string> see_also { get; set; }
        public List<object> antonyms { get; set; }
        public List<object> source { get; set; }
        public List<string> info { get; set; }
        public List<object> sentences { get; set; }
    }


}
