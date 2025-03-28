namespace AzureDocumentIntelligence.Models
{
    public class AnalyzeResponse
    {
        public string ServiceVersion { get; set; }
        public string ModelId { get; set; }
        public string Content { get; set; }
        public Document[] Documents { get; set; }
    }

    public class Document
    {
        public Dictionary<string, FieldData> Fields { get; set; }
    }

    public class FieldData
    {
        public string Content { get; set; }
        public decimal Confidence { get; set; }
    }
}
