using System.IO;
using System.Web.Hosting;

namespace HttpEnv
{
    public class Content
    {
        public string Shout(string localUrlPath)
        {
            string physicalPath = HostingEnvironment.MapPath(localUrlPath);
            string content = File.ReadAllText(physicalPath);
            return content.ToUpper();
        }
    }
}
