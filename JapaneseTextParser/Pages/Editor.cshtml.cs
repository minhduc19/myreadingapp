using JapaneseTextParserDomain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http.Extensions;
using JapaneseTextParser.Service;

namespace JapaneseTextParser.Pages
{
    public class EditorModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public Text returnText { get; set; }
        [BindProperty]
        public List<string> paragraphList { get; set; }
        [BindProperty]
        public string[] paragraphs { get; set; } = { };
        public string a;
        [BindProperty]
        public List<WordPair> listOfWordPair { get; set; } = new List<WordPair>();
        
        public async Task OnGet()
        {
            var sentenceSegment = new SentenceSegment();
            listOfWordPair = await sentenceSegment.CrawMaintext(returnText.content);
            if (string.IsNullOrWhiteSpace(returnText.content)) {
                paragraphList = new List<string>();
            }
            else {
                paragraphs = returnText.content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                // Create a list to store the paragraphs
                paragraphList = new List<string>(paragraphs);
            }

            a = UriHelper.GetDisplayUrl(Request);
        }
        public async Task<IActionResult> OnPost()
        {
            // Split the input text into paragraphs using the newline character "\n" as the delimiter

            
            var formContent = Request.Form["content"];
            return RedirectToPage("./Editor", new { content = returnText.content }) ;
        }
    }
}
