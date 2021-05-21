using System;
using System.Reflection;
using log4net;

namespace SDEToolsServer
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static int Main(string[] args)
        {
            Application.Application app = new Application.Application();

            return app.Run();


        }
    }
}
