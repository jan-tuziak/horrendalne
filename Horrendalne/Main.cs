using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Horrendalne
{
    [TestFixture]
    public class Main
    {
        protected IBrowserContext? Context { get; set; }
        protected IPage Page { get; set; }

        [SetUp]
        public async Task Setup()
        {
            // Setup Playwright
            var playwright = await Playwright.CreateAsync();
#if DEBUG
            var launchOptions = new BrowserTypeLaunchOptions{ Headless = false };
#else
            var launchOptions = new BrowserTypeLaunchOptions{ Headless = true };
#endif
            var browser = await playwright.Chromium.LaunchAsync(launchOptions);
            Context = await browser.NewContextAsync();
            Page = await Context.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            // Close browser instance
            if (Context != null)
            {
                await Context.CloseAsync();
                Context = null;
            }
        }

        [Test]
        public async Task GenerateWebsite()
        {
            var jobListGenerator = new JobListGenerator(Page);
            var jobs = await jobListGenerator.GenerateJobList();

            var indexGenerator = new IndexHtmlGenerator(jobs);
            indexGenerator.GenerateIndexHtml();
            Console.WriteLine("Website generated successfilly");
        }

        
    }
}