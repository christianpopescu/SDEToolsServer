using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace SDEToolsServer.Application
{
    /// <summary>
    /// This class represents the application
    /// 
    /// </summary>
    public class Application
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public int Run()
        {
            log4net.Config.XmlConfigurator.Configure();
            ThreadContext.Stacks["NDC"].Push("Application");
            log.Info("----- SDEToolsServer - Started! ------");
            return 0;
        }
    }
}
