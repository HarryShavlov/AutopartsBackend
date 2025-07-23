using AutopartsBackend.API.Models;
using Microsoft.AspNetCore.Http;

namespace AutopartsBackend.API.Services
{
    public interface IRecognitionService
    {
        Task<RecognitionResultDto> RecognizePart(IFormFile image);
    }
}