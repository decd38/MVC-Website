using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DrivingLessonsSite.Startup))]
namespace DrivingLessonsSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
