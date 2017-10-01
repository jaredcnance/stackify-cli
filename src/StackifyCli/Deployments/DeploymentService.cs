using System.Collections.Generic;
using System.Threading.Tasks;
using StackifyCli.Client;
using StackifyCli.Extensions;
using StackifyCli.Options;

namespace StackifyCli.Deployments
{
    public interface IDeploymentService
    {
        Task<List<Deployment>> GetAsync(DeployOptions options);
        Task StartAsync(DeployOptions options);
        Task CancelAsync(DeployOptions options);
        Task CompleteAsync(DeployOptions options);
    }

    public class DeploymentService : IDeploymentService
    {
        private readonly IApiClient _client;
        private const string ROUTE = "api/v1/deployments";

        public DeploymentService(IApiClient client)
        {
            _client = client;
        }

        public async Task<List<Deployment>> GetAsync(DeployOptions options)
        {
            var config = new ApiClientConfig(options.ApiKey);
            var deployments = await _client.GetAsync<List<Deployment>>(config, ROUTE);

            deployments = deployments
                .Filter(d => d.App, options.App)
                .Filter(d => d.Environment, options.Environment)
                .Filter(d => d.Version, options.Version);

            return deployments;
        }

        public async Task StartAsync(DeployOptions options)
        {
            var config = new ApiClientConfig(options.ApiKey);
            var payload = new
            {
                name = options.Name,
                uri = options.Uri,
                branch = options.Branch,
                commit = options.Commit,
                version = options.Version,
                appName = options.App,
                environmentName = options.Environment
            };
            await _client.PostAsync(config, ROUTE + "/start", payload);
        }

        public async Task CancelAsync(DeployOptions options)
        {
            var config = new ApiClientConfig(options.ApiKey);
            var payload = new
            {
                version = options.Version,
                appName = options.App,
                environmentName = options.Environment
            };

            await _client.PostAsync(config, ROUTE + "/cancel", payload);
        }

        public async Task CompleteAsync(DeployOptions options)
        {
            var config = new ApiClientConfig(options.ApiKey);
            var payload = new
            {
                name = options.Name,
                uri = options.Uri,
                branch = options.Branch,
                commit = options.Commit,
                version = options.Version,
                appName = options.App,
                environmentName = options.Environment
            };
            await _client.PostAsync(config, ROUTE + "/complete", payload);
        }
    }
}