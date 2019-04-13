using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TB.Web.Startup))]
namespace TB.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
