using System;
namespace JieDDDFramework.Core.Configures
{
    public class BaseConfig
    {
        public Connectionstrings ConnectionStrings { get; set; }
        public bool IsClusterEnv { get; set; } = false;

        public string RedisConnectionString { get; set; }
        public Logging Logging { get; set; }
    }
}
