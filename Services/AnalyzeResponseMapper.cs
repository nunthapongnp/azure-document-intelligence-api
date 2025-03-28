using Azure.AI.FormRecognizer.DocumentAnalysis;
using AzureDocumentIntelligence.Models;

public static class AnalyzeResponseMapper
{
    public static AnalyzeResponse MapToAnalyzeResponse(AnalyzeResult analyzeResult)
    {
        return new AnalyzeResponse
        {
            ServiceVersion = analyzeResult.ServiceVersion,
            ModelId = analyzeResult.ModelId,
            Content = analyzeResult.Content,
            Documents = analyzeResult.Documents.Select(d => new Document
            {
                Fields = d.Fields.ToDictionary(f => f.Key, f => new FieldData
                {
                    Content = f.Value.Content,
                    Confidence = (decimal)f.Value.Confidence,
                })
            }).ToArray()
        };
    }
}
