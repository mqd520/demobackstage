using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// SimpleShell
    /// </summary>
    public class SimpleShell
    {
        protected bool ignoreCase;
        protected string[] arrExitCmd = new string[] { "exit", "quit" };

        /// <summary>
        /// Exit Event
        /// </summary>
        public Action<SimpleShell> Exit;

        /// <summary>
        /// Cmd Event
        /// </summary>
        public event Action<SimpleShell, string> Cmd;

        /// <summary>
        /// Detail Cmd
        /// </summary>
        public event Action<SimpleShell, DetailCmdParam> DetailCmd;


        public SimpleShell(bool ignoreCase = false)
        {
            this.ignoreCase = ignoreCase;
            DetailCmdAssemblies.Add(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Get or Set Exit Cmd
        /// </summary>
        public string[] ExitCmd
        {
            get
            {
                return arrExitCmd;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    arrExitCmd = value;
                }
            }
        }

        /// <summary>
        /// Get DetailCmd Assemblies
        /// </summary>
        public List<Assembly> DetailCmdAssemblies { get; private set; } = new List<Assembly>();

        /// <summary>
        /// Start
        /// </summary>
        public virtual void Start()
        {
            while (true)
            {
                string line = Console.ReadLine();

                bool b = OnReadLine(line);
                if (!b)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Read Line Event
        /// </summary>
        /// <param name="line">line</param>
        protected virtual bool OnReadLine(string line)
        {
            bool result = true;

            foreach (var item in arrExitCmd)
            {
                if (string.Compare(line, item, ignoreCase) == 0)
                {
                    OnExit();
                    result = false;
                    break;
                }
            }

            if (result)
            {
                try
                {
                    OnCmd(line);
                }
                catch (Exception e)
                {
                    ConsoleHelper.WriteLine(ELogCategory.Fatal, "OnCmd Error");
                    CommonLogger.WriteLog(ELogCategory.Fatal, "OnCmd Error", e);
                }

                ProcessDetailCmd(line);
            }

            return result;
        }

        /// <summary>
        /// OnExit
        /// </summary>
        protected virtual void OnExit()
        {
            if (Exit != null)
            {
                Exit.Invoke(this);
            }
        }

        /// <summary>
        /// OnCmd
        /// </summary>
        /// <param name="cmd"></param>
        protected virtual void OnCmd(string cmd)
        {
            if (Cmd != null)
            {
                Cmd.Invoke(this, cmd);
            }
        }

        /// <summary>
        /// Process Detail Cmd
        /// </summary>
        /// <param name="cmd"></param>
        protected virtual void ProcessDetailCmd(string cmd)
        {
            if (!string.IsNullOrEmpty(cmd))
            {
                string[] arr = cmd.Split(' ');

                DetailCmdParam param = new Common.DetailCmdParam();
                param.Cmd = "";
                param.Params = new Dictionary<string, string>();

                if (arr.Length > 0)
                {
                    param.Cmd = arr[0];

                    bool bException = false;
                    try
                    {
                        Regex reg = new Regex("\\s+-([a-zA-Z0-9]{1,15})", RegexOptions.Multiline);
                        MatchCollection mc = reg.Matches(cmd);
                        for (int i = 0; i < mc.Count; i++)
                        {
                            string key = mc[i].Value.Replace(" ", "").Replace("-", "");

                            int startIndex = mc[i].Index + mc[i].Length;
                            int length = 0;
                            if (i + 1 < mc.Count)
                            {
                                length = mc[i + 1].Index - mc[i].Index - mc[i].Length;
                            }
                            else
                            {
                                length = cmd.Length - mc[i].Index - mc[i].Length;
                            }
                            string value = cmd.Substring(startIndex, length).Trim();

                            param.Params.Add(key, value);
                        }
                    }
                    catch (Exception e)
                    {
                        bException = true;

                        ConsoleHelper.WriteLine(
                            ELogCategory.Warn,
                            string.Format("Invalid cmd!")
                        );

                        CommonLogger.WriteLog(
                            ELogCategory.Warn,
                            string.Format("Invalid cmd: {0}", cmd),
                            e
                        );
                    }

                    if (!bException)
                    {
                        try
                        {
                            OnDetailCmd(param);
                        }
                        catch (Exception e)
                        {
                            ConsoleHelper.WriteLine(ELogCategory.Fatal, "OnDetailCmd Error");
                            CommonLogger.WriteLog(ELogCategory.Fatal, "OnDetailCmd Error", e);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// On Detail Cmd
        /// </summary>
        /// <param name="param"></param>
        protected virtual void OnDetailCmd(DetailCmdParam param)
        {
            if (DetailCmd != null)
            {
                DetailCmd.Invoke(this, param);
            }

            ProcessDetailCmd(param);
        }

        /// <summary>
        /// ProcessDetailCmd
        /// </summary>
        /// <param name="param"></param>
        protected virtual void ProcessDetailCmd(DetailCmdParam param)
        {
            Type t = typeof(IDetailCmd);
            List<Type> lsType = new List<Type>();
            foreach (var item in DetailCmdAssemblies)
            {
                Type[] arr = item.GetTypes();
                lsType.AddRange(arr);
            }
            Type[] types = lsType.ToArray();

            List<Type> ls = new List<Type>();
            foreach (var item in types)
            {
                Type[] ts = item.GetInterfaces();
                if (ts != null && ts.Any(x => x == t))
                {
                    ls.Add(item);
                }
            }

            if (!ls.Any())
            {
                ConsoleHelper.WriteLine(ELogCategory.Warn, string.Format("Undefined Cmd: {0}", param.Cmd));
            }
            else
            {
                bool bExist = false;

                foreach (var item in ls)
                {
                    object o = Activator.CreateInstance(item);
                    IDetailCmd obj = o as IDetailCmd;
                    if (obj != null)
                    {
                        if (obj.Cmd.Equals(param.Cmd))
                        {
                            bExist = true;
                            try
                            {
                                obj.OnDetailCmd(param.Params);
                            }
                            catch (Exception e)
                            {
                                ConsoleHelper.WriteLine(ELogCategory.Fatal, "OnDetailCmd error");
                                CommonLogger.WriteLog(ELogCategory.Fatal, "OnDetailCmd Exception", e);
                            }
                        }
                    }
                }

                if (!bExist)
                {
                    ConsoleHelper.WriteLine(ELogCategory.Warn, string.Format("Undefined Cmd: {0}", param.Cmd));
                }
            }
        }
    }
}