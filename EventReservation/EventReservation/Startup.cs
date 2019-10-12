using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventReservation.Startup))]
namespace EventReservation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
