using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;
using System.Collections;

namespace ServiceStack.Redis.Utils
{
    public class RedisConfigurationSection : ConfigurationSection
    {
        #region Property
        /// <summary>
        /// Max Write Pool Size
        /// </summary>
        [ConfigurationProperty("maxWritePoolSize", DefaultValue = 5, IsRequired = false)]
        public int MaxWritePoolSize
        {
            get
            {
                return (int)this["maxWritePoolSize"];
            }
            set
            {
                this["maxWritePoolSize"] = value;
            }
        }

        /// <summary>
        /// Max Read Pool Size
        /// </summary>
        [ConfigurationProperty("maxReadPoolSize", DefaultValue = 5, IsRequired = false)]
        public int MaxReadPoolSize
        {
            get
            {
                return (int)this["maxReadPoolSize"];
            }
            set
            {
                this["maxReadPoolSize"] = value;
            }
        }

        /// <summary>
        /// Auto Start
        /// </summary>
        [ConfigurationProperty("autoStart", DefaultValue = true, IsRequired = false)]
        public bool AutoStart
        {
            get
            {
                return (bool)this["autoStart"];
            }
            set
            {
                this["autoStart"] = value;
            }
        }

        /// <summary>
        /// Read Write Hosts
        /// </summary>
        [ConfigurationProperty("readWriteHosts", DefaultValue = "127.0.0.1:6379", IsRequired = false)]
        public string ReadWriteHosts
        {
            get
            {
                return (string)this["readWriteHosts"];
            }
            set
            {
                this["readWriteHosts"] = value;
            }
        }

        /// <summary>
        /// Read Only Hosts
        /// </summary>
        [ConfigurationProperty("readOnlyHosts", DefaultValue = "127.0.0.1:6379", IsRequired = false)]
        public string ReadOnlyHosts
        {
            get
            {
                return (string)this["readOnlyHosts"];
            }
            set
            {
                this["readOnlyHosts"] = value;
            }
        }

        /// <summary>
        /// Is KeepAlive
        /// </summary>
        [ConfigurationProperty("isKeepAlive", DefaultValue = true, IsRequired = false)]
        public bool IsKeepAlive
        {
            get
            {
                return (bool)this["isKeepAlive"];
            }
            set
            {
                this["isKeepAlive"] = value;
            }
        }

        /// <summary>
        /// Get KeepAlive Interval
        /// </summary>
        [ConfigurationProperty("keepAliveInterval", DefaultValue = 10000, IsRequired = false)]
        public int KeepAliveInterval
        {
            get
            {
                return (int)this["keepAliveInterval"];
            }
            set
            {
                this["keepAliveInterval"] = value;
            }
        }

        /// <summary>
        /// Is Sentinel Mode
        /// </summary>
        [ConfigurationProperty("isSentinel", DefaultValue = false, IsRequired = false)]
        public bool IsSentinel
        {
            get
            {
                return (bool)this["isSentinel"];
            }
            set
            {
                this["isSentinel"] = value;
            }
        }

        /// <summary>
        /// Sentinel Hosts
        /// </summary>
        [ConfigurationProperty("sentinelHosts", DefaultValue = "", IsRequired = false)]
        public string SentinelHosts
        {
            get
            {
                return (string)this["sentinelHosts"];
            }
            set
            {
                this["sentinelHosts"] = value;
            }
        }

        /// <summary>
        /// Sentinel Node Pwd
        /// </summary>
        [ConfigurationProperty("sentinelNodePwd", DefaultValue = "", IsRequired = false)]
        public string SentinelNodePwd
        {
            get
            {
                return (string)this["sentinelNodePwd"];
            }
            set
            {
                this["sentinelNodePwd"] = value;
            }
        }
        #endregion
    }
}
