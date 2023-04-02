using Core.Dtos.Oboseala;
using Core.Utils.ObosealaApi;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using MimeKit.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Utils;
using Org.BouncyCastle.Utilities;

namespace Core.Services
{
    public class ObosealaService
    {
        public static int index = 0;
        public static void SaveImageFromBase64String(string base64String, string filePath)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);

            using (var stream = new MemoryStream(imageBytes))
            {
                var image = System.Drawing.Image.FromStream(stream);
                image.Save(filePath);
            }
        }
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        public async Task<IsObositDto> MakeCall(string file, string type)
        {
            var filePath = Path.Combine($"./", $"poza{index}.{type}");
            SaveImageFromBase64String(file, filePath);

            index++;

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://10.0.0.40:5000/predict");
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(File.OpenRead(filePath)), "pic", $"poza{index}.{type}");
                request.Content = content;

                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<FacialLandmarks>(jsonString);

                if (result == null)
                    throw new WrongInputException("Result is null");

                var dto = new IsObositDto { IsObosit = false };

                if (result.final_prediction < 570)
                    dto.IsObosit = true;

                return dto;
            }
        }
    }

}
