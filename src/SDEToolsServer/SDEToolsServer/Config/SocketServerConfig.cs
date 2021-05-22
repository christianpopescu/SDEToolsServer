using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDEToolsServer.Config
{
    public class SocketServerConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("port",
            IsRequired = true,
            IsKey = false)]
        public string Port
        {

            get
            {
                return (string)this["port"];
            }
            set
            {
                this["port"] = value;
            }
        }
    }
}
