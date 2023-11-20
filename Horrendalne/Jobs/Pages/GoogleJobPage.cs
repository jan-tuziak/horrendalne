using Microsoft.Playwright;

namespace Horrendalne.Pages
{
    public class GoogleJobPage : JobPage
    {
        private ILocator jobsOnPage => Page.Locator("css=c-wiz > div > ul > li");
        private ILocator nextPageButton => Page.GetByRole(AriaRole.Button, new() { Name = "Go to next page" });
        
        public GoogleJobPage(IPage page, string url) : base(page, url, "Google")
        {

        }


        public override async Task<List<Job>> GetJobs()
        {
            await Page.GotoAsync(Url);

            await nextPageButton.ClickAsync();

            return new List<Job>();
        }
    }
}