using AnimalFarmsMarket.Data.DTOs;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnimalFarmsMarket.Commons
{
    public static class PaystackPayment
    {
        /// <summary>
        /// Use this method to initiate payment
        /// </summary>
        /// <param name="model"></param>
        /// <param name="baseUrl"></param>
        /// <param name="partOfUrl"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<PaystackReturnDto> InitiatePayment(PaystackRequestDto model, string baseUrl,string partOfUrl,string token)
        {
            var response = await PostContentWithAuthorizationAsync<PaystackRequestDto>(baseUrl, model, partOfUrl, token);

            if (response.Item2.IsSuccessStatusCode)
            {
                var result = await response.Item2.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PaystackReturnDto>(result);
            }
            return null;
        }

        /// <summary>
        /// Use this method to verify payment before giving value to customer
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="baseUrl"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<bool> VerifyPayment(string reference, string baseUrl, string token)
        {
            using (var _client = new HttpClient())
            {
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await _client.GetAsync(baseUrl + "verify/" + reference);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        private static Tuple<string, StringContent> BuildReqContent<T>(HttpClient client, string _baseURL, T model, string PartOfUrl)
        {
            client.BaseAddress = new Uri(_baseURL);
            client.DefaultRequestHeaders.Accept.Clear();

            var serializedModel = JsonConvert.SerializeObject(model);

            var currentUrl = Path.Combine(client.BaseAddress.ToString(), PartOfUrl);

            var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
            return new Tuple<string, StringContent>(currentUrl, content);
        }

        private static async Task<Tuple<string, HttpResponseMessage>> PostContentWithAuthorizationAsync<T>(string _baseURL, T model, string PartOfUrl, string token)
        {
            var response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var contents = BuildReqContent<T>(client, _baseURL, model, PartOfUrl);
                response = await client.PostAsync(contents.Item1, contents.Item2);
            }
            var responseObj = await response.Content.ReadAsStringAsync();
            return new Tuple<string, HttpResponseMessage>(responseObj, response);
        }
    }
}
