using HttpEnv;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Web.Hosting;

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

            var test = new Content(new MockUrlMapper(filePath));
            
            string result = test.Shout(url);

            Assert.AreEqual(content.ToUpper() + "!", result);
        }

        public class MockUrlMapper : IUrlMapper
        {
            private readonly string _filePath;

            public MockUrlMapper(string filePath)
            {
                _filePath = filePath;
            }

            public string Map(string localUrlPath)
            {
                return _filePath;
            }
        }

        [TestMethod]
        public void ShoutIntegrationTest()
        {
            string fileName = "bar.txt";

            string filePath = Path.Combine(Path.GetTempPath(), fileName);
            string content = "original content";
            File.WriteAllText(filePath, content);

            string url = "/" + fileName;

            var test = new Content();

            MockMapPath(url, filePath);

            string result = test.Shout(url);

            Assert.IsNotNull(result);
        }

        private void MockMapPath(string url, string filePath)
        {
            var hostingEnvironment = new HostingEnvironment();
            var vpType = typeof(HostingEnvironment).Assembly.GetType("System.Web.VirtualPath");
            var createMethod = vpType.GetMethod("Create", new[] { typeof(string) });
            var virtualPath = createMethod.Invoke(null, new[] { url });
            SetPrivateField(hostingEnvironment, "_appVirtualPath", virtualPath);
            SetPrivateField(hostingEnvironment, "_appPhysicalPath", filePath);
        }

        private void SetPrivateField(HostingEnvironment hostingEnvironment, string fieldName, object value)
        {
            var fieldInfo = hostingEnvironment.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(hostingEnvironment, value);
        }
    }
}
