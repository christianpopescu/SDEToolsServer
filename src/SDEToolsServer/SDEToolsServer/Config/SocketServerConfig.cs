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
        private const string TagPort = "port";
        private const string TagBacklog = "backlog";

        [ConfigurationProperty(TagPort,
            IsRequired = true,
            IsKey = false)]
        public string Port
        {

            get
            {
                return (string)this[TagPort];
            }
            set
            {
                this[TagPort] = value;
            }
        }

        [ConfigurationProperty(TagBacklog,
            IsRequired = true,
            IsKey = false)]
        public string Backlog
        {

            get
            {
                return (string)this[TagBacklog];
            }
            set
            {
                this[TagBacklog] = value;
            }
        }
    }
}
