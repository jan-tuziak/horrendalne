using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horrendalne.Pages
{
    public abstract class JobPage
    {
        public IPage Page { get; }
        protected readonly string Url;
        protected readonly string CompanyName;

        protected JobPage(IPage page, string url, string companyName)
        {
            Page = page;
            Url = url;
            CompanyName = companyName;
        }

        public async Task<List<Job>> GetJobs()
        {
            await Page.GotoAsync(Url);

            var jobs = await ScrapeJobs();
            await Console.Out.WriteLineAsync($"Scraped {jobs.Count} jobs from {CompanyName}");

            return jobs;
        }

        protected abstract Task<List<Job>> ScrapeJobs();
    }
}
