using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NameSearch.Startup))]
namespace NameSearch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
