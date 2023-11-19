namespace Horrendalne
{
    public class IndexHtmlGenerator
    {
        public string IndexHtmlTemplatePath { get; private set; }
        public string IndexHtmlPath { get; private set; }
        public IndexHtmlGenerator()
        {
            int count = GetNumberOfUpwardsFolders();

            IndexHtmlPath = string.Concat(Enumerable.Repeat("../", count)) + "output/index.html";
            IndexHtmlTemplatePath = string.Concat(Enumerable.Repeat("../", count)) + "index.html.template";
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

        private static int GetNumberOfUpwardsFolders()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var count = -1;
            for (int i = 1; i < 6; i++)
            {
                currentDir = Directory.GetParent(currentDir)?.FullName;
                if (currentDir == null)
                {
                    Console.WriteLine("Reached root folder.");
                    break;
                }

                var files = Directory.EnumerateFiles(currentDir, "*.sln", SearchOption.TopDirectoryOnly);

                if (files.Count() > 0)
                {
                    count = i;
                }
            }

            if (count < 0)
            {
                throw new Exception("Cannot find root folder");
            }

            return count;
        }
    }
}
