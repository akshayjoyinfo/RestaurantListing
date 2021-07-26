using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InHouse.Tests.Integration
{
    [ExcludeFromCodeCoverage]
    public static class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static async Task<string> GetResponseStringContent(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            return stringResponse;
        }

        public static async Task<T> GetRequestObjectFromJsonFile<T>(string jsonFile, string folder)
        {
            var outputFolder = Path.Combine(Environment.CurrentDirectory, folder);

            var filePath = Path.Combine(outputFolder, jsonFile);

            string textContent = await File.ReadAllTextAsync(filePath);

            return JsonConvert.DeserializeObject<T>(textContent);

        }

        public static async Task<string> GetFileContent(string jsonFile, string folder)
        {
            var outputFolder = Path.Combine(Environment.CurrentDirectory, folder);

            var filePath = Path.Combine(outputFolder, jsonFile);

            string textContent = await File.ReadAllTextAsync(filePath);

            return textContent;

        }
    }
}
