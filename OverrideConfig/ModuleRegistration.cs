using System.Web;

[assembly: PreApplicationStartMethod(typeof(OverrideConfig.ModuleRegistration), "Initialize")]

namespace OverrideConfig
{
    public class ModuleRegistration
    {
        public static void Initialize()
        {
            HttpApplication.RegisterModule(typeof(OverrideConfigHttpModule));
        }
    }
}
