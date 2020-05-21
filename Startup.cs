using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EverestAlbumLibrary.Startup))]
namespace EverestAlbumLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
