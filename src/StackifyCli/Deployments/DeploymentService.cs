using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackifyCli.Client;

namespace StackifyCli.Deployments
{
    public interface IDeploymentService
    {
        Task<List<Deployment>> GetAllAsync(string apiKey);
        Task<List<Deployment>> GetAsync(string apiKey, string appName, string environment);
        Task StartAsync(string apiKey, string appName, string environment);
        Task CancelAsync(string apiKey, string appName, string environment);
        Task CompleteAsync(string apiKey, string appName, string environment);
    }

    public class DeploymentService : IDeploymentService
    {
        private readonly IApiClient _client;

        public DeploymentService(IApiClient client)
        {
            _client = client;
        }

        public Task CancelAsync(string apiKey, string appName, string environment)
        {
            throw new System.NotImplementedException();
        }

        public Task CompleteAsync(string apiKey, string appName, string environment)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Deployment>> GetAllAsync(string apiKey)
        {
            var config = new ApiClientConfig(apiKey);
            return await _client.GetAsync<List<Deployment>>(config, "api/v1/deployments");
        }

        public async Task<List<Deployment>> GetAsync(string apiKey, string appName, string environment)
        {
            var config = new ApiClientConfig(apiKey);
            var deployments = await _client.GetAsync<List<Deployment>>(config, "api/v1/deployments");
            return deployments.Where(d => d.App == appName && d.Environment == environment).ToList();
        }

        public Task StartAsync(string apiKey, string appName, string environment)
        {
            throw new System.NotImplementedException();
        }
    }
}