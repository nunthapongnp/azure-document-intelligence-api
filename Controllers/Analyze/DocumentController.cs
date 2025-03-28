using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using AzureDocumentIntelligence.Models;
using AzureDocumentIntelligence.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureDocumentIntelligence.Controllers.Analyze
{
    public class DocumentController : BaseController
    {
        private readonly AppService _appService;
        private readonly DocumentAnalysisClient _client;

        public DocumentController(AppService appService)
        {
            _appService = appService;
            _client = this._appService.GetClient();
        }

        [HttpPost("analyze-bytes/{modelId}")]
        public async Task<IActionResult> AnalyzeDocumentFromBytes([FromBody] ApiRequest request, string modelId)
        {
            if (request.Attachments == null || request.Attachments.Count == 0)
            return BadRequest(new ApiResponse(new ApiError { Message = "Attachments cannot be empty." }));


            List<AnalyzeResponse> results = new List<AnalyzeResponse>();

            try
            {
                foreach (var attachment in request.Attachments)
                {
                    using var stream = new MemoryStream(attachment);
                    AnalyzeDocumentOperation operation = await _client.AnalyzeDocumentAsync(WaitUntil.Completed, modelId, stream);
                    var analyzeResponse = AnalyzeResponseMapper.MapToAnalyzeResponse(operation.Value);
                    results.Add(analyzeResponse);
                }

                return Ok(new ApiResponse<List<AnalyzeResponse>>(results));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(new ApiError { Message = ex.Message }));
            }
        }

        [HttpPost("analyze-url/{modelId}")]
        public async Task<IActionResult> AnalyzeDocumentFromUrl([FromBody] string documentUrl, string modelId)
        {
            try
            {
                AnalyzeDocumentOperation operation = await _client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, modelId, new Uri(documentUrl));
                var analyzeResponse = AnalyzeResponseMapper.MapToAnalyzeResponse(operation.Value);
                return Ok(new ApiResponse<AnalyzeResponse>(analyzeResponse));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(new ApiError { Message = ex.Message }));
            }
        }

        [HttpPost("analyze-file/{modelId}")]
        public async Task<IActionResult> AnalyzeDocumentFromFile(IFormFile file, string modelId)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new ApiResponse(new ApiError { Message = "File is empty." }));

            try
            {
                using var stream = file.OpenReadStream();
                AnalyzeDocumentOperation operation = await _client.AnalyzeDocumentAsync(WaitUntil.Completed, modelId, stream);
                AnalyzeResponse analyzeResponse = AnalyzeResponseMapper.MapToAnalyzeResponse(operation.Value);
                return Ok(new ApiResponse<AnalyzeResponse>(analyzeResponse));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(new ApiError { Message = ex.Message }));
            }
        }
    }
}
