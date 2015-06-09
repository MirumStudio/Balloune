/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

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

        //For reference only (Don't compile for windows store app and add considerable compile time for toher platform
        /*
        public static List<Type> GetAllTypeFromNamespace(string _namespace)
        {
            return (from type in Assembly.GetExecutingAssembly().GetTypes()
                    where type.IsClass && type.Namespace.Contains(_namespace)
                    select type).ToList();
        }

        public static List<Type> GetAllTypeFromNamespace(Type _type, string _namespace)
        {
            return (from type in Assembly.GetExecutingAssembly().GetTypes()
                    where type.IsClass && type.Namespace.Contains(_namespace) && type.IsSubclassOf(_type)
                    select type).ToList();
        }
        */
    }
}
