using Microsoft.Playwright;

namespace Horrendalne.Pages
{
    public static class JobPageMapper
    {
        public static List<JobPage> MapJobPage(IPage page, Dictionary<string, string> jobPagesUrls)
        {
            var jobPages = new List<JobPage>();

            foreach (var jobPageUrl in jobPagesUrls)
            {
                jobPages.Add(MapJobPage(page, jobPageUrl.Key, jobPageUrl.Value));
            }

            return jobPages;
        }

        public static JobPage MapJobPage(IPage page, string jobPageName, string jobPagesUrl)
        {
            return jobPageName switch
            {
                "google" => new GoogleJobPage(page, jobPagesUrl),
                _ => throw new NotImplementedException($"Could not map page \"{jobPageName}\" with url \"{jobPagesUrl}\""),
            };
        }
    }
}