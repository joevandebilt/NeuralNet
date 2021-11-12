using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NeuralNet.Db;

namespace NeuralNet.Webroot.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly NeuralNetTrainingContext _dbContext;

        public IndexModel(ILogger<IndexModel> logger, NeuralNetTrainingContext context)
        {
            _logger = logger;
            _dbContext = context;
    }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public string? text { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    var category = _dbContext.NgramCategories.First();
                    IList<String> words = text.Split(new string[] { " ", Environment.NewLine, "\r\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    
                    for (int i = 0; i < words.Count - 1; i++)
                    {
                        string word = $"{words[i]} {words[i + 1]}";
                        var ngram = _dbContext.Ngrams.FirstOrDefault(n => n.Value == word);
                        if (ngram == null)
                        {
                            _dbContext.Ngrams.Add(new Db.Entities.NGram
                            {
                                CategoryId = category.Id,
                                Value = word,
                                Frequency = 1
                            });
                        }
                        else
                        {
                            ngram.Frequency++;
                        }
                    }

                    await _dbContext.SaveChangesAsync();

                    text = string.Empty;
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }
            return Page();
        }
    }
}