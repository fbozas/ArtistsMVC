using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtistsMVC.Startup))]
namespace ArtistsMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
