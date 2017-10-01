namespace StackifyCli.Client
{
    public class ApiClientConfig
    {
        public ApiClientConfig(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string Host { get; set; } = "http://api,stackify.net";
        public string ApiKey { get; set; }
    }
}