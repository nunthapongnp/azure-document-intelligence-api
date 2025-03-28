using System.Text.Json.Serialization;

namespace AzureDocumentIntelligence.Models
{
    public class ApiRequest
    {
        [JsonPropertyName("attachments")]
        public List<byte[]> Attachments { get; set; } = new();
    }
}
