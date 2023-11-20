﻿using Horrendalne.Pages;

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

            var indexText = ReplaceVariablesInTemplate(template);

            File.WriteAllText(IndexHtmlPath, indexText);
        }

        private string ReplaceVariablesInTemplate(string template)
        {
            template = template.Replace("{{date}}", DateTime.Now.ToString());
            return template;
        }
    }
}
