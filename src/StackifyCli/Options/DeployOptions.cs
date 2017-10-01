using McMaster.Extensions.CommandLineUtils;

namespace StackifyCli.Options
{
    public class DeployOptions : ApiOptions
    {
        public static DeployOptions GetOptions(CommandLineApplication cmd)
        {
            cmd.HelpOption("-?");

            return new DeployOptions(cmd)
            {
                _appOption = cmd.Option("-a | --app", "Application name (STACKIFY_CLI_APP)", CommandOptionType.SingleValue),
                _environmentOption = cmd.Option("-e | --env", "Environment name (STACKIFY_CLI_ENV)", CommandOptionType.SingleValue),
                _versionOption = cmd.Option("-v | --version", "Version (STACKIFY_CLI_VERSION)", CommandOptionType.SingleValue),
                _prettyOption = cmd.Option("-p | --pretty", "Pretty (STACKIFY_CLI_PRETTY)", CommandOptionType.SingleValue)
            };
        }

        private DeployOptions(CommandLineApplication cmd) : base(cmd) { }

        private CommandOption _appOption { get; set; }
        private CommandOption _environmentOption { get; set; }
        private CommandOption _versionOption { get; set; }
        private CommandOption _prettyOption { get; set; }

        public string App => _appOption.HasValue() ? _appOption.Value() : Config.Get("APP");
        public string Environment => _appOption.HasValue() ? _appOption.Value() : Config.Get("ENVIRONMENT");
        public string Version => _appOption.HasValue() ? _appOption.Value() : Config.Get("VERSION");
        public bool Pretty
        {
            get
            {
                var option = _prettyOption.HasValue() ? _prettyOption.Value() : Config.Get("PRETTY");
                bool.TryParse(option, out bool pretty);
                return pretty;
            }
        }
    }
}