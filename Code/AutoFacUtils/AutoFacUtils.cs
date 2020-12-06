using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Autofac;

namespace AutoFacUtils
{
    public class AutoFacHelper
    {
        /// <summary>
        /// Container
        /// </summary>
        public static IContainer Container { get; private set; }


        static AutoFacHelper()
        {
            
        }

        /// <summary>
        /// Init
        /// </summary>
        public static void Init(IList<string> lsAssembly)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .AsImplementedInterfaces().AsSelf();

            foreach (var item in lsAssembly)
            {
                Assembly assem1 = Assembly.Load(item);
                builder.RegisterAssemblyTypes(assem1)
                       .AsImplementedInterfaces().AsSelf();
            }

            Container = builder.Build();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
