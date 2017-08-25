using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarketLearning.Startup))]
namespace MarketLearning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
