using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horrendalne.Pages;
using Microsoft.Playwright;
using FluentAssertions;

namespace Horrendalne
{
    public class JobListGenerator
    {
        private readonly List<JobPage> _jobPages;
        private readonly IPage _page;

        public JobListGenerator(IPage Page)
        {
            _page = Page;
            _jobPages = new List<JobPage>()
            {
                new GoogleJobPage(_page)
            };
        }

        public async Task<List<Job>> GenerateJobList()
        {
            var jobs = new List<Job>();

            foreach(var jobPage in _jobPages)
            {
                jobs.AddRange(await jobPage.GetJobs());
            }
            await Console.Out.WriteLineAsync($"Scraped {jobs.Count} jobs in total");
            return jobs;
        }
    }
}
