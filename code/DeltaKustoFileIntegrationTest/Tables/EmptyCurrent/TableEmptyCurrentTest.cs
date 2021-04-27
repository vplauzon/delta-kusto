using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DeltaKustoFileIntegrationTest.EmptyTarget
{
    public class TableEmptyCurrentTest : IntegrationTestBase
    {
        [Fact]
        public async Task EmptyDelta()
        {
            var parameters = await RunParametersAsync(
                "Tables/EmptyCurrent/EmptyDelta/empty-delta-params.yaml",
                CreateCancellationToken());
            var outputPath = parameters.Jobs!.First().Value.Action!.FilePath!;
            var commands = await LoadScriptAsync(outputPath);

            Assert.Empty(commands);
        }

        [Fact]
        public async Task OneTableDelta()
        {
            var parameters = await RunParametersAsync(
                "Tables/EmptyCurrent/OneTableDelta/delta-params.yaml",
                CreateCancellationToken());
            var inputPath = parameters.Jobs!.First().Value.Target!.Scripts!.First().FilePath!;
            var outputPath = parameters.Jobs!.First().Value.Action!.FilePath!;
            var inputCommands = await LoadScriptAsync(inputPath);
            var outputCommands = await LoadScriptAsync(outputPath);

            Assert.True(inputCommands.SequenceEqual(outputCommands));
        }

        [Fact]
        public async Task TwoFunctionsDelta()
        {
            var parameters = await RunParametersAsync(
                "Tables/EmptyCurrent/TwoTablesDelta/delta-params.yaml",
                CreateCancellationToken());
            var inputPath = parameters.Jobs!.First().Value.Target!.Scripts!.First().FilePath!;
            var outputPath = parameters.Jobs!.First().Value.Action!.FilePath!;
            var inputCommands = await LoadScriptAsync(inputPath);
            var outputCommands = await LoadScriptAsync(outputPath);

            Assert.True(inputCommands.SequenceEqual(outputCommands));
        }

        private CancellationToken CreateCancellationToken() =>
           new CancellationTokenSource(TimeSpan.FromSeconds(2)).Token;
    }
}