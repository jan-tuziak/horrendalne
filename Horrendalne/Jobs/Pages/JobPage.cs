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

        public abstract Task<List<Job>> GetJobs();
    }
}
