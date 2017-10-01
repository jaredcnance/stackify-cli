using McMaster.Extensions.CommandLineUtils;

namespace StackifyCli.Options
{
    public class DeployOptions : ApiOptions
    {
        public static DeployOptions GetOptions(CommandLineApplication cmd)
        {
            cmd.HelpOption("-?| -h | --help");

            return new DeployOptions(cmd)
            {
                _appOption = cmd.Option("-a | --app", "Application name [string] (STACKIFY_CLI_APP)", CommandOptionType.SingleValue),
                _environmentOption = cmd.Option("-e | --env", "Environment name [string] (STACKIFY_CLI_ENV)", CommandOptionType.SingleValue),
                _versionOption = cmd.Option("-v | --version", "Version [string] (STACKIFY_CLI_VERSION)", CommandOptionType.SingleValue),
                _prettyOption = cmd.Option("-p | --pretty", "Pretty [flag]", CommandOptionType.NoValue)
            };
        }

        private DeployOptions(CommandLineApplication cmd) : base(cmd) { }

        private CommandOption _appOption { get; set; }
        private CommandOption _environmentOption { get; set; }
        private CommandOption _versionOption { get; set; }
        private CommandOption _prettyOption { get; set; }

        public string App => _appOption.HasValue() ? _appOption.Value() : Config.Get("APP");
        public string Environment => _environmentOption.HasValue() ? _environmentOption.Value() : Config.Get("ENVIRONMENT");
        public string Version => _versionOption.HasValue() ? _versionOption.Value() : Config.Get("VERSION");
        public bool Pretty => _prettyOption.HasValue();
    }
}