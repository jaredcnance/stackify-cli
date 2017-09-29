using McMaster.Extensions.CommandLineUtils;

namespace StackifyCli.Options
{
    public class ApiOptions
    {
        public ApiOptions(CommandLineApplication cmd)
        {
            _apiKeyOption = cmd.Option("-k | --key", "Your Stackify API key (STACKIFY_CLI_APIKEY)", CommandOptionType.SingleValue);
        }

        private CommandOption _apiKeyOption { get; set; }
        public string ApiKey => _apiKeyOption.HasValue() ? _apiKeyOption.Value() : Config.Get("APIKEY");
    }
}