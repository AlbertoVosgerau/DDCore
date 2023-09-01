using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DandyDino.Core
{
    public static class ReflectionUtility
    {
        public static List<Type> GetAllClassesOfType<T>()
        {
            List<Type> result = new List<Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var queryType = typeof(T);

            foreach (Assembly assembly in assemblies)
            {
                Type[] typesInAssembly = assembly.GetTypes();

                foreach (Type type in typesInAssembly)
                {
                    Type baseType = type.BaseType;
                    if (baseType == null || baseType.FullName == null || type.FullName == null)
                    {
                        continue;
                    }
                    if (baseType.IsGenericType)
                    {
                        Type[] tArgs = queryType.GetGenericArguments();
                        Type[] args = baseType.GetGenericArguments();
                        bool argsEqual = tArgs.Length > 0 && args.Length > 0 && tArgs[0] == args[0].BaseType;
                        // This is HACKY
                        bool isSubclass = baseType.FullName.Split('`')[0] == queryType.FullName.Split('`')[0];

                        if (isSubclass && argsEqual && !type.IsAbstract)
                        {
                            result.Add(type);
                        }
                    }
                }
            }

            return result;
        }

        public static List<T> GetInstancesOfAllClassesOfType<T>() where T : Object
        {
            List<T> result = new List<T>();
            List<Type> types = GetAllClassesOfType<T>();

            foreach (Type type in types)
            {
                object instance= (T)Activator.CreateInstance(type);
                result.Add(instance as T);
            }

            return result;
        }
    }
}