using System;

namespace StackifyCli
{
    public static class Config
    {
        private static string EnvVarPrefix = "STACKIFY_CLI";
        public static string Get(string key) => Environment.GetEnvironmentVariable($"{EnvVarPrefix}_{key}");
    }
}
