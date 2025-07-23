using AutopartsBackend.API.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Json;

namespace AutopartsBackend.API.Services
{
    public class RecognitionService : IRecognitionService
    {
        private readonly HttpClient _httpClient;

        public RecognitionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RecognitionResultDto> RecognizePart(IFormFile image)
        {
            try
            {
                // 1. Проверка файла
                if (image == null || image.Length == 0)
                    throw new ArgumentException("Invalid image file");

                // 2. Подготовка контента
                using var stream = image.OpenReadStream();
                using var content = new MultipartFormDataContent();
                content.Add(new StreamContent(stream), "image", image.FileName);

                // 3. Отправка запроса
                var response = await _httpClient.PostAsync("recognize", content);

                // 4. Обработка ответа
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"ML service error: {error}");
                }

                // 5. Десериализация
                return await response.Content.ReadFromJsonAsync<RecognitionResultDto>() 
                    ?? throw new Exception("Invalid response from ML service");
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error in RecognizePart: {ex.Message}");
                throw;
            }
        }
    }
}