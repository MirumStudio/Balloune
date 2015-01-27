using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Radix.Utilities
{
    public class TypeUtility
    {
        public static bool IsInNamespace(Type _type, string _namespace)
        {
            return _type.Namespace.Contains(@_namespace);
        }

        public static List<Type> GetAllTypeFromNamespace(string _namespace)
        {
            return null;
            /*return (from type in Assembly.GetExecutingAssembly().GetTypes()
                    where type.IsClass && type.Namespace.Contains(_namespace)
                    select type).ToList();*/
        }

        public static List<Type> GetAllTypeFromNamespace(Type _type, string _namespace)
        {
            return null;/*
            return (from type in Assembly.GetExecutingAssembly().GetTypes()
                    where type.IsClass && type.Namespace.Contains(_namespace) && type.IsSubclassOf(_type)
                    select type).ToList();*/
        }
    }
}
