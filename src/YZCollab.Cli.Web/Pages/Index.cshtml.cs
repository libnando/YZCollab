using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YZCollab.Cli.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public readonly IConfiguration Config;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            Config = configuration;
        }

        public void OnGet()
        {

        }
    }
}