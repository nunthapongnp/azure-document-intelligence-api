using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure;
using Microsoft.Extensions.Options;
using AzureDocumentIntelligence.Models;

namespace AzureDocumentIntelligence.Services
{
    public class AppService
    {
        private readonly DocumentAnalysisClient _client;

        public AppService(IOptions<AzureSettings> options)
        {
            var settings = options.Value;
            _client = new DocumentAnalysisClient(new Uri(settings.Endpoint), new AzureKeyCredential(settings.ApiKey));
        }

        public DocumentAnalysisClient GetClient()
        {
            return _client;
        }
    }
}
