using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace JieDDDFramework.Data.EntityFramework.ModelConfigurations
{
    public class ModelConfigurationOption
    {
        /// <summary>
        /// 默认过滤字段集合
        /// </summary>
        public List<string> QueryFilterFields = new List<string>() {"IsDeleted"};

        /// <summary>
        /// 自动注册时根据不同的DbContext类型来指定其<see cref="IEntityTypeConfiguration{TEntity}"/>所在的命名空间 />
        /// </summary>
        public Dictionary<Type,string> DbModelConfigurationNamespaceDictionary = new Dictionary<Type, string>();
    }
}
