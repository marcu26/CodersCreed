﻿using Core.Dtos.Oboseala;
using Core.Utils.ObosealaApi;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ObosealaService
    {
        public async Task<IsObositDto> MakeCall(Microsoft.AspNetCore.Http.IFormFile file) 
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://10.0.0.40:5000/predict");
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(file.OpenReadStream()), "pic", file.FileName);
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
