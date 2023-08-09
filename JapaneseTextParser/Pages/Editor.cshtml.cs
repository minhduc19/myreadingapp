using JapaneseTextParserDomain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http.Extensions;
using JapaneseTextParser.Service;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
        [BindProperty]
        public string testValue { get; set; }
        [BindProperty]
        public List<WordPair> listOfWordPair { get; set; } = new List<WordPair>();
        
        public async Task OnGet()
        {
            testValue = HttpContext.Session.GetString("Test String");

            if (string.IsNullOrWhiteSpace(returnText.content)) {
                paragraphList = new List<string>();
            }
            else {
                paragraphs = returnText.content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                
            }

            //a = UriHelper.GetDisplayUrl(Request);
        }
        public async Task<IActionResult> OnPost()
        {
            // Split the input text into paragraphs using the newline character "\n" as the delimiter

            
            var formContent = Request.Form["content"];
            var sentenceSegment = new SentenceSegment();
            listOfWordPair = await sentenceSegment.CrawMaintext(formContent);
            var jsonString = JsonConvert.SerializeObject(listOfWordPair);
            HttpContext.Session.SetString("Test String", jsonString);
            return RedirectToPage("./Editor") ;
        }
    }
}
