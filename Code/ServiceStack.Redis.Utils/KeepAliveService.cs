using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;

using Common;

using ServiceStack.Redis.Utils._00_Def;
using ServiceStack.Redis.Utils._02_Common;

namespace ServiceStack.Redis.Utils
{
    public class KeepAliveService
    {
        #region Field
        private object _obj = new object();

        private IList<IRedisClient> _lsRedisClients = new List<IRedisClient>();

        private IList<StatusInfo> _lsStatusInfos = new List<StatusInfo>();

        private Timer _tKeepAlive = new Timer();
        #endregion


        #region Property
        /// <summary>
        /// Get Instance
        /// </summary>
        public static KeepAliveService Instance { get; private set; } = null;
        #endregion


        #region Constructor
        private KeepAliveService()
        {
            _tKeepAlive.AutoReset = false;
            _tKeepAlive.Elapsed += _tKeepAlive_Elapsed;
        }

        static KeepAliveService()
        {
            if (Instance == null)
            {
                Instance = new KeepAliveService();
            }
        }
        #endregion


        #region Method
        /// <summary>
        /// Init
        /// </summary>
        public void Init()
        {
            RedisConfigurationSection redis = ConfigurationManager.GetSection("redis") as RedisConfigurationSection;
            _tKeepAlive.Interval = redis.KeepAliveInterval;
            string[] arrR = redis.ReadOnlyHosts.Split(',').Select(x => x.Trim()).ToArray();
            foreach (var item in arrR)
            {
                var info = HostInfoTool.Parse(item);
                _lsRedisClients.Add(new RedisClient(info.Ip, info.Port, info.Pwd));

                _lsStatusInfos.Add(new StatusInfo
                {
                    Addr = string.Format("{0}:{1}", info.Ip, info.Port),
                    IsOnline = true
                });
            }

            _tKeepAlive.Start();
        }

        /// <summary>
        /// Exit
        /// </summary>
        public void Exit()
        {
            _tKeepAlive.Stop();
            _tKeepAlive.Dispose();
        }

        /// <summary>
        /// Is Avaiable
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool IsAvaiable(IRedisClient client)
        {
            string addr = string.Format("{0}:{1}", client.Host, client.Port);
            var item = _lsStatusInfos.FirstOrDefault(x => x.Addr.Equals(addr));
            if (item != null)
            {
                return item.IsOnline;
            }

            return false;
        }
        #endregion


        #region Event Callback
        private void _tKeepAlive_Elapsed(object sender, ElapsedEventArgs e)
        {
            _tKeepAlive.Stop();

            IList<StatusInfo> ls = new List<StatusInfo>();

            foreach (var item in _lsRedisClients)
            {
                bool bOnline = false;
                string addr = string.Format("{0}:{1}", item.Host, item.Port);

                try
                {
                    bOnline = item.Ping();
                }
                catch (Exception e1)
                {
                    ConsoleHelper.WriteLine(
                        ELogCategory.Warn,
                        string.Format("Miss Connection With {0}", addr),
                        true,
                        e: e1
                    );
                }

                ls.Add(new StatusInfo
                {
                    Addr = addr,
                    IsOnline = bOnline
                });
            }

            foreach (var item in ls)
            {
                var item1 = _lsStatusInfos.FirstOrDefault(x => x.Addr.Equals(item.Addr));
                if (item1 != null)
                {
                    item1.IsOnline = item.IsOnline;
                }
                else
                {
                    _lsStatusInfos.Add(new StatusInfo
                    {
                        Addr = item.Addr,
                        IsOnline = item.IsOnline
                    });
                }
            }

            _tKeepAlive.Start();
        }
        #endregion
    }

    internal class StatusInfo
    {
        public string Addr { get; set; }

        public bool IsOnline { get; set; }
    }
}
