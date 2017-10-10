using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BTC_R.Startup))]
namespace BTC_R
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
