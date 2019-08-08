using System.IO;
using System.Web.Hosting;

namespace HttpEnv
{
    public class Content
    {
        private readonly IUrlMapper _mapper;

        public Content(IUrlMapper mapper)
        {
            _mapper = mapper;
        }

        public Content() : this(new HostingEnvironmentUrlMapper()) { }

        public string Shout(string localUrlPath)
        {
            string physicalPath = _mapper.Map(localUrlPath);
            string content = File.ReadAllText(physicalPath);
            return content.ToUpper() + "!";
        }
    }

    public interface IUrlMapper
    {
        string Map(string localUrlPath);
    }

    public class HostingEnvironmentUrlMapper : IUrlMapper
    {
        public string Map(string localUrlPath)
        {
            return HostingEnvironment.MapPath(localUrlPath);
        }
    }
}
