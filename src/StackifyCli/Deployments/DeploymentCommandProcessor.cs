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
            var deployments = await _service.GetAsync(options);
            Write(deployments, options.Pretty);
        }

        public async Task CompleteAsync(DeployOptions options) => await _service.CompleteAsync(options);
        public async Task StartAsync(DeployOptions options) => await _service.StartAsync(options);
        public async Task CancelAsync(DeployOptions options) => await _service.CancelAsync(options);

        private void Write(object content, bool pretty)
        {
            if (pretty)
                _writer.WritePretty(content);
            else
                _writer.Write(content);
        }
    }
}