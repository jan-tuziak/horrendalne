using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horrendalne.Pages
{
    public abstract class JobPage
    {
        protected string Url;

        public JobPage(string url)
        {
            Url = url;
        }
    }
}
