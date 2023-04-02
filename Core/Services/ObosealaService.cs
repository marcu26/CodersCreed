using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ObosealaService
    {
        public async Task<string> MakeCall(Microsoft.AspNetCore.Http.IFormFile file) 
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://10.0.0.40:5000/predict");
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(file.OpenReadStream()), "pic", file.FileName);
                request.Content = content;

                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
