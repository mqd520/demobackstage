using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Reflection;

using Microsoft.Web.Redis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using DemoBackStage.Def;

namespace DemoBackStage.Web.Common
{
    public class MySessionSerializer : ISerializer
    {
        public object Deserialize(byte[] data)
        {
            if (data != null && data.Length > 0)
            {
                string json = Encoding.UTF8.GetString(data);
                if (!string.IsNullOrEmpty(json))
                {
                    var obj = JsonConvert.DeserializeObject(json);
                    var jo = obj as JObject;
                    if (jo != null)
                    {
                        if (TypeHelper.IsUserInfo(jo))
                        {
                            return jo.ToObject<UserInfo>();
                        }

                        return jo;
                    }

                    return obj;
                }
            }

            return null;
        }

        public byte[] Serialize(object data)
        {
            if (data != null)
            {
                JsonSerializerSettings jss = new JsonSerializerSettings();
                jss.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                string json = JsonConvert.SerializeObject(data, jss);
                if (!string.IsNullOrEmpty(json))
                {
                    return Encoding.UTF8.GetBytes(json);
                }
            }

            return new byte[0];
        }
    }


    internal static class TypeHelper
    {
        internal static Type _tUserInfo = null;


        static TypeHelper()
        {

        }

        internal static bool IsMatch(JObject jo, Type type)
        {
            bool bIsMatch = true;

            PropertyInfo[] pis = type.GetProperties();
            foreach (var item in pis)
            {
                if (!jo.ContainsKey(item.Name))
                {
                    bIsMatch = false;

                    break;
                }
            }

            return bIsMatch;
        }

        internal static bool IsUserInfo(JObject jo)
        {
            if (_tUserInfo == null)
            {
                _tUserInfo = typeof(UserInfo);
            }

            return IsMatch(jo, _tUserInfo);
        }
    }
}