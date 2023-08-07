namespace JapaneseTextParser.Model
{
    public class WordPair
    {
        public WordPair(string _word, string _furigana)
        {
            word = _word;
            furigana = _furigana;
        }
        public string word { get; set; }
        public string furigana { get; set; }
    }
}
