using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteCredentials.Common
{
    /// <summary>
    /// Very basic DI controller with named instances
    /// </summary>
    public class ObjectFactory
    {
        private static Dictionary<Type, Type> _classes = new Dictionary<Type, Type>();
        private static Dictionary<Type, Dictionary<string, Type>> _namedClasses = new Dictionary<Type, Dictionary<string, Type>>();
        private static Dictionary<Type, object> _singletons = new Dictionary<Type, object>();

        public static void Register(Type source, Type dest)
        {
            _classes.Add(source, dest);
            _singletons.Add(source, Activator.CreateInstance(dest));
        }

        public static void Register<S, D>()
            where D : S
        {
            _classes.Add(typeof(S), typeof(D));
            _singletons.Add(typeof(S), Activator.CreateInstance(typeof(D)));
        }

        public static void Register<S, D>(string instanceName)
            where D : S
        {
            if(!_namedClasses.ContainsKey(typeof(S)))
                _namedClasses.Add(typeof(S), new Dictionary<string, Type>());

            _namedClasses[typeof(S)].Add(instanceName, typeof(D));
        }

        public static object GetInstance(Type source)
        {
            return Activator.CreateInstance(_classes[source]);
        }

        public static T GetInstance<T>()
        {
            return (T)Activator.CreateInstance(_classes[typeof(T)]);
        }

        public static T GetNamedInstance<T>(string instanceName)
        {
            return (T)Activator.CreateInstance(_namedClasses[typeof(T)][instanceName]);
        }

        public static object GetSingleton(Type source)
        {
            return _singletons[source];
        }

        public static T GetSingleton<T>()
        {
            return (T)_singletons[typeof(T)];
        }

        public static void Clean()
        {
            _classes = new Dictionary<Type, Type>();
            _singletons = new Dictionary<Type, object>();
            _namedClasses = new Dictionary<Type, Dictionary<string, Type>>();
        }
    }
}
