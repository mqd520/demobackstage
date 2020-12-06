using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Common
{
    /// <summary>
    /// Console Init
    /// </summary>
    public class ConsoleInit
    {
        public ConsoleInit()
        {

        }

        protected virtual void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var e1 = e.ExceptionObject as Exception;
            CommonLogger.WriteLog(
                ELogCategory.Fatal,
                string.Format("ConsoleInit.OnUnhandledException: {0}", e1.Message),
                e: e1
            );
            if (e.IsTerminating)
            {
                Environment.Exit(1);
            }
        }

        protected virtual void OnInit()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        protected virtual void OnWhile()
        {
            while (true)
            {
                string line = Console.ReadLine();
                if (string.Compare(line, "exit", StringComparison.OrdinalIgnoreCase) == 0 ||
                    string.Compare(line, "quit", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    break;
                }
            }
        }

        protected virtual void OnExit()
        {
            ConsoleHelper.WriteLine(ELogCategory.Info, "Program will be exited");
        }

        /// <summary>
        /// Run
        /// </summary>
        public virtual void Run()
        {
            OnInit();

            OnWhile();

            OnExit();
        }
    }
}
