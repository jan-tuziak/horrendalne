using Microsoft.Playwright;
using Newtonsoft.Json;

namespace Horrendalne.Pages
{
    public class GoogleJobPage : JobPage
    {
        private ILocator jobsOnPage => Page.Locator("css=c-wiz > div > ul > li");
        private ILocator nextPageButton => Page.GetByRole(AriaRole.Button, new() { Name = "Go to next page" });
        private readonly Uri UrlPrefix = new Uri("https://www.google.com/about/careers/applications/");

        public GoogleJobPage(IPage page) : base(page, "https://www.google.com/about/careers/applications/jobs/results/?location=Poland", "Google")
        {

        }

        protected override async Task<List<Job>> ScrapeJobs()
        {
            var jobs = new List<Job>();

            // Iterate over all pages
            while(!await nextPageButton.IsDisabledAsync(new LocatorIsDisabledOptions() { Timeout = 10 * 1000 }))
            {
                jobs.AddRange(await ScrapeJobsOnPage());
                await nextPageButton.ClickAsync();
            }
            jobs.AddRange(await ScrapeJobsOnPage());

            return jobs;
        }

        private async Task<List<Job>> ScrapeJobsOnPage()
        {
            var jobList = new List<Job>();
            var jobs = await jobsOnPage.AllAsync();
            foreach (var job in jobs)
            {
                try
                {
                    var jobTitle = await job.GetByRole(AriaRole.Heading).And(job.Locator("css=h3")).InnerTextAsync();
                    var jobUrl = new Uri(UrlPrefix, await job.GetByRole(AriaRole.Link).GetAttributeAsync("href"));
                    jobList.Add(new Job() { Title = jobTitle, Url = jobUrl.ToString(), Company = CompanyName});
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not scrape this specific job from Google. " + e);
                }
            }
            return jobList;
        }
    }
}