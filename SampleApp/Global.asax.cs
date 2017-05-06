using System;
using System.Configuration;

namespace SampleApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var test = ConfigurationManager.AppSettings["TEST"];
        }
    }
}