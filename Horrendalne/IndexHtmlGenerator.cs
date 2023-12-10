using Horrendalne.Pages;
using Newtonsoft.Json;

namespace Horrendalne
{
    public class IndexHtmlGenerator
    {
        public string DistFolderPath { get; private set; }
        public string IndexHtmlTemplatePath { get; private set; }
        public string IndexHtmlPath { get; private set; }
        private List<Job> Jobs { get; set; }

        public IndexHtmlGenerator(List<Job> jobs)
        {
            Jobs = jobs;
            DistFolderPath = "dist";
            IndexHtmlPath = DistFolderPath + "/index.html";
            IndexHtmlTemplatePath = "index.html.template";

            Directory.CreateDirectory(DistFolderPath);
        }

        public void GenerateIndexHtml()
        {
            string template = File.ReadAllText(IndexHtmlTemplatePath);
            template = InsertJobs(template);
            var indexText = ReplaceVariablesInTemplate(template);

            File.WriteAllText(IndexHtmlPath, indexText);
        }

        private string InsertJobs(string template)
        {
            var jobsHtml = new List<string>();
            Jobs.ForEach(job => jobsHtml.Add($"<a href={job.Url}>{job.Title} @ {job.Company}</a>"));
            return template.Replace("{{jobs}}", string.Join("</br>", jobsHtml));
        }

        private string ReplaceVariablesInTemplate(string template)
        {
            template = template.Replace("{{date}}", DateTime.Now.ToString());
            return template;
        }
    }
}
