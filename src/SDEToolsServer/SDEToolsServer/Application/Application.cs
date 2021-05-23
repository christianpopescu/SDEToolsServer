using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SDEToolsServer.Config;
using SocketServer;

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

        protected CmdSocketServer SocketServer { get; set; }

        protected void Init()
        {
            log4net.Config.XmlConfigurator.Configure();
            SocketServer = CmdSocketServer.GetSimpleSocketServer(Config.GetSection("SocketServer") as SocketServerConfigSection);
            

        }

        public int Run()
        {
            this.Init();
            SocketServer.Listen();
            ThreadContext.Stacks["NDC"].Push("Application");
            log.Info("----- SDEToolsServer - Started! ------");
            return 0;
        }
    }
}
