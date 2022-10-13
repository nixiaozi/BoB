﻿using BoB.BoBConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BoB.EFDbContext
{
    public static class BaseDbContext
    {
        /// <summary>
        /// 初始化数据模型注入
        /// </summary>
        /// <param name="modelBuilder">模型创建者</param>
        /// <param name="type">所选类型</param>
        public static void Init(ModelBuilder modelBuilder,Type type)
        {
            var NotProjectNamesList = StaticConfiguration.NotProjectNameArray().ToList();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(s =>
                    {
                        bool flag = true;
                        int i = 0;
                        while (flag && i < NotProjectNamesList.Count)
                        {
                            flag = !s.FullName.StartsWith(NotProjectNamesList[i]);
                            i++;
                        }
                        return flag;
                    }).ToArray();

            List<Type> typesToRegister = new List<Type>();
            Type modelType = type;
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(s => !String.IsNullOrEmpty(s.Namespace))
                    .Where(s => s.BaseType != null && !s.IsGenericType && modelType.IsAssignableFrom(s));

                typesToRegister.AddRange(types);
            }

            if (typesToRegister.Count > 0)
            {
                foreach (var t in typesToRegister)
                {
                    IModelCreator creator = Activator.CreateInstance(t) as IModelCreator;
                    creator.CreateModel(modelBuilder);
                }
            }

        }

    }
}
