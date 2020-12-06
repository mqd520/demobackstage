using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace Common
{
    /// <summary>
    /// Common Logger
    /// </summary>
    public static class CommonLogger
    {
        private static readonly ILog debugLogger = null;
        private static readonly ILog infoLogger = null;
        private static readonly ILog warnLogger = null;
        private static readonly ILog errorLogger = null;
        private static readonly ILog fatalLogger = null;
        private static readonly ILog sqlLogger = null;
        private static readonly ILog recvLogger = null;
        private static readonly ILog pushLogger = null;


        static CommonLogger()
        {
            debugLogger = log4net.LogManager.GetLogger("myDebugLogger");
            infoLogger = log4net.LogManager.GetLogger("myInfoLogger");
            warnLogger = log4net.LogManager.GetLogger("myWarnLogger");
            errorLogger = log4net.LogManager.GetLogger("myErrorLogger");
            fatalLogger = log4net.LogManager.GetLogger("myFatalLogger");
            sqlLogger = log4net.LogManager.GetLogger("mySqlLogger");
            recvLogger = log4net.LogManager.GetLogger("myRecvLogger");
            pushLogger = log4net.LogManager.GetLogger("myPushLogger");
        }

        #region Property
        /// <summary>
        /// Get Debug Logger
        /// </summary>
        public static ILog DebugLogger
        {
            get
            {
                return debugLogger;
            }
        }

        /// <summary>
        /// Get Info Logger
        /// </summary>
        public static ILog InfoLogger
        {
            get
            {
                return infoLogger;
            }
        }

        /// <summary>
        /// Get Warn Logger
        /// </summary>
        public static ILog WarnLogger
        {
            get
            {
                return warnLogger;
            }
        }

        /// <summary>
        /// Get Error Logger
        /// </summary>
        public static ILog ErrorLogger
        {
            get
            {
                return errorLogger;
            }
        }

        /// <summary>
        /// Get Fatal Logger
        /// </summary>
        public static ILog FatalLogger
        {
            get
            {
                return fatalLogger;
            }
        }

        /// <summary>
        /// Get Sql Logger
        /// </summary>
        public static ILog SqlLogger
        {
            get
            {
                return sqlLogger;
            }
        }

        /// <summary>
        /// Get Recv Logger
        /// </summary>
        public static ILog RecvLogger
        {
            get
            {
                return recvLogger;
            }
        }

        /// <summary>
        /// Get Push Logger
        /// </summary>
        public static ILog PushLogger
        {
            get
            {
                return pushLogger;
            }
        }
        #endregion

        /// <summary>
        /// Write Log
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="log">log</param>
        /// <param name="e">exception</param>
        public static void WriteLog(ELogCategory category, object log = null, Exception e = null)
        {
            switch (category)
            {
                case ELogCategory.Debug:
                    if (debugLogger.IsDebugEnabled)
                    {
                        if (log != null)
                        {
                            if (e != null)
                            {
                                debugLogger.Debug(log, e);
                            }
                            else
                            {
                                debugLogger.Debug(log);
                            }
                        }
                        else
                        {
                            if (e != null)
                            {
                                debugLogger.Debug(e);
                            }
                        }
                    }
                    break;
                case ELogCategory.Info:
                    if (infoLogger.IsInfoEnabled)
                    {
                        if (log != null)
                        {
                            if (e != null)
                            {
                                infoLogger.Info(log, e);
                            }
                            else
                            {
                                infoLogger.Info(log);
                            }
                        }
                        else
                        {
                            if (e != null)
                            {
                                infoLogger.Info(e);
                            }
                        }
                    }
                    break;
                case ELogCategory.Warn:
                    if (WarnLogger.IsWarnEnabled)
                    {
                        if (log != null)
                        {
                            if (e != null)
                            {
                                WarnLogger.Warn(log, e);
                            }
                            else
                            {
                                WarnLogger.Warn(log);
                            }
                        }
                        else
                        {
                            if (e != null)
                            {
                                WarnLogger.Warn(e);
                            }
                        }
                    }
                    break;
                case ELogCategory.Error:
                    if (errorLogger.IsErrorEnabled)
                    {
                        if (log != null)
                        {
                            if (e != null)
                            {
                                errorLogger.Error(log, e);
                            }
                            else
                            {
                                errorLogger.Error(log);
                            }
                        }
                        else
                        {
                            if (e != null)
                            {
                                errorLogger.Error(e);
                            }
                        }
                    }
                    break;
                case ELogCategory.Fatal:
                    if (fatalLogger.IsFatalEnabled)
                    {
                        if (log != null)
                        {
                            if (e != null)
                            {
                                fatalLogger.Fatal(log, e);
                            }
                            else
                            {
                                fatalLogger.Fatal(log);
                            }
                        }
                        else
                        {
                            if (e != null)
                            {
                                fatalLogger.Fatal(e);
                            }
                        }
                    }
                    break;
                case ELogCategory.Sql:
                    if (sqlLogger.IsInfoEnabled)
                    {
                        if (log != null)
                        {
                            if (e != null)
                            {
                                sqlLogger.Info(log, e);
                            }
                            else
                            {
                                sqlLogger.Info(log);
                            }
                        }
                        else
                        {
                            if (e != null)
                            {
                                sqlLogger.Info(e);
                            }
                        }
                    }
                    break;
                case ELogCategory.Recv:
                    if (recvLogger.IsInfoEnabled)
                    {
                        if (log != null)
                        {
                            if (e != null)
                            {
                                recvLogger.Info(log, e);
                            }
                            else
                            {
                                recvLogger.Info(log);
                            }
                        }
                        else
                        {
                            if (e != null)
                            {
                                recvLogger.Info(e);
                            }
                        }
                    }
                    break;
                case ELogCategory.Push:
                    if (pushLogger.IsInfoEnabled)
                    {
                        if (log != null)
                        {
                            if (e != null)
                            {
                                pushLogger.Info(log, e);
                            }
                            else
                            {
                                pushLogger.Info(log);
                            }
                        }
                        else
                        {
                            if (e != null)
                            {
                                pushLogger.Info(e);
                            }
                        }
                    }
                    break;
            }
        }
    }
}
