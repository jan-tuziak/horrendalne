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
            var jobUrlsFileName = @"jobUrls.json";
            File.Exists(jobUrlsFileName).Should().BeTrue("Check if \"jobUrls.json\" file exists.");
            var jobPagesUrls = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(jobUrlsFileName));
            jobPagesUrls.Should().NotBeNullOrEmpty("Check \"jobUrls.json\" file. Read zero urls from it.");

            _page = Page;
            _jobPages = JobPageMapper.MapJobPage(_page, jobPagesUrls);
        }

        public async Task<List<Job>> GenerateJobList()
        {
            var jobs = new List<Job>();

            foreach(var jobPage in _jobPages)
            {
                jobs.AddRange(await jobPage.GetJobs());
            }

            return jobs;
        }
    }
}
