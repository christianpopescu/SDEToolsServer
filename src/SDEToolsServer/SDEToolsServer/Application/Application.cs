using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SDEToolsServer.Config;

namespace SDEToolsServer.Application
{
    /// <summary>
    /// This class represents the application
    /// 
    /// </summary>
    public class Application
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // To do: Create a more generic configuration intialization (builder or injector)
        public Application()
        {
            Config = ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None); 
        }

        public System.Configuration.Configuration Config
        {
            get; private set;
        }

        public int Run()
        {
            log4net.Config.XmlConfigurator.Configure();
            var ssc = Config.GetSection("SocketServer") as SocketServerConfigSection;
            log.Info("PORT" + ssc.Port);
            ThreadContext.Stacks["NDC"].Push("Application");
            log.Info("----- SDEToolsServer - Started! ------");
            return 0;
        }
    }
}
