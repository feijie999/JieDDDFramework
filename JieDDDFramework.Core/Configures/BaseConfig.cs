using System;
namespace JieDDDFramework.Core.Configures
{
    public class BaseConfig
    {
        public string ConnectionString { get; set; }
        public bool IsClusterEnv { get; set; } = false;

        public string RedisConnectionString { get; set; }
        public Logging Logging { get; set; }
    }
}
