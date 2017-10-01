using System.Collections.Generic;
using System.Threading.Tasks;
using StackifyCli.Client;

namespace StackifyCli.Deployments
{
    public interface IDeploymentService
    {
        Task<List<Deployment>> GetAllAsync(string apiKey);
        Task<List<Deployment>> GetAsync(string apiKey, string AppName, string EnvironmentId);
        Task StartAsync(string apiKey, string AppName, string EnvironmentId);
        Task CancelAsync(string apiKey, string AppName, string EnvironmentId);
        Task CompleteAsync(string apiKey, string AppName, string EnvironmentId);
    }

    public class DeploymentService : IDeploymentService
    {
        private readonly IApiClient _client;

        public DeploymentService(IApiClient client)
        {
            _client = client;
        }

        public Task CancelAsync(string apiKey, string AppName, string EnvironmentId)
        {
            throw new System.NotImplementedException();
        }

        public Task CompleteAsync(string apiKey, string AppName, string EnvironmentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Deployment>> GetAllAsync(string apiKey)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Deployment>> GetAsync(string apiKey, string AppName, string EnvironmentId)
        {
            var config = new ApiClientConfig(apiKey);
            return await _client.GetAsync<List<Deployment>>(config, "api/v1/deployments");
        }

        public Task StartAsync(string apiKey, string AppName, string EnvironmentId)
        {
            throw new System.NotImplementedException();
        }
    }
}