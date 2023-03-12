using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace TechFix.Common
{
    public static class RestClientExtensions
    {
        public static async Task<T> ExecutePostAsync<T>(object postData, string url, Dictionary<string, string> headers = null)
        {
            var restClient = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (postData != null)
            {
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var jsonObject = JsonConvert.SerializeObject(postData, Formatting.Indented, jsonSerializerSettings);

                request.AddParameter("application/json", jsonObject, ParameterType.RequestBody);
            }

            var taskCompletion = new TaskCompletionSource<IRestResponse>();

            restClient.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            var response = (RestResponse)(await taskCompletion.Task);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
        public static async Task<T> ExecuteGetAsync<T>(string url, Dictionary<string, string> headers = null)
        {
            var restClient = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            var taskCompletion = new TaskCompletionSource<IRestResponse>();

            restClient.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            var response = (RestResponse)(await taskCompletion.Task);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public static async Task<T> ExecuteVlinkMartPostAsync<T>(object postData, string url, Dictionary<string, string> headers = null)
        {
            var restClient = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (postData != null)
            {
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var jsonObject = JsonConvert.SerializeObject(postData, Formatting.Indented, jsonSerializerSettings);
                
                request.AddParameter("body", jsonObject);
            }

            var taskCompletion = new TaskCompletionSource<IRestResponse>();

            restClient.ExecuteAsync(request, r => taskCompletion.SetResult(r));
            var response = (RestResponse)(await taskCompletion.Task);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}