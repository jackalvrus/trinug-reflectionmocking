using HttpEnv;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace HttpEnvTests
{
    [TestClass]
    public class ContentTests
    {
        [TestMethod]
        public void ShoutTest()
        {
            string fileName = "bar.txt";

            string filePath = Path.Combine(Path.GetTempPath(), fileName);
            string content = "original content";
            File.WriteAllText(filePath, content);

            string url = "/" + fileName;

            var test = new Content();

            //TODO: somehow get HostingEnvironment.MapPath to convert url to filePath

            string result = test.Shout(url);

            Assert.AreEqual(content.ToUpper(), result);
        }
    }
}
