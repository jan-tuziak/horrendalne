namespace Horrendalne
{
    public class Main
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GenerateWebsite()
        {
            var indexGenerator = new IndexHtmlGenerator();
            indexGenerator.GenerateIndexHtml();
        }

        
    }
}