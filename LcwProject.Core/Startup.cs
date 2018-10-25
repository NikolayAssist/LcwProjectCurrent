using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LcwProject.Core.Startup))]
namespace LcwProject.Core
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
