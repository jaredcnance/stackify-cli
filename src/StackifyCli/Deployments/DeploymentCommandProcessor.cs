using System;
using System.Threading.Tasks;
using StackifyCli.Terminal;
using StackifyCli.Options;

namespace StackifyCli.Deployments
{
    public class DeploymentCommandProcessor
    {
        private readonly IDeploymentService _service;
        private readonly IConsoleWriter _writer;

        public DeploymentCommandProcessor(
            IDeploymentService service,
            IConsoleWriter writer)
        {
            _service = service;
            _writer = writer;
        }

        public async Task GetAsync(DeployOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.App) || string.IsNullOrWhiteSpace(options.Environment))
            {
                var deployments = await _service.GetAllAsync(options.ApiKey);
                Write(deployments, options.Pretty);
            }
            else
            {
                var deployments = await _service.GetAsync(options.ApiKey, options.App, options.Environment);
                Write(deployments, options.Pretty);
            }
        }

        public void New(DeployOptions options)
        {
            throw new NotImplementedException();
        }

        private void Write(object content, bool pretty)
        {
            if (pretty)
                _writer.WritePretty(content);
            else
                _writer.Write(content);
        }
    }
}