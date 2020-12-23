using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineCourse.Startup))]
namespace OnlineCourse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
