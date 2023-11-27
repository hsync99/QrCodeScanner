using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeScanner.Services
{
    public class RestClientService : IRestClient
    {
        public HttpClient Client = new HttpClient(); 
        public async Task<string> GetDocumentData(string address)
        {
            
            try
            {
                // var json = JsonConvert.SerializeObject(commands, Formatting.Indented);
                // StringContent content = new StringContent(json);
                // определяем данные запроса
                var request = new HttpRequestMessage(HttpMethod.Get, address);
                // установка отправляемого содержимого
                //request.Content = content;
                // отправляем запрос
                var response = await Client.GetAsync(address);
                // получаем ответ
                string responseText = await response.Content.ReadAsStringAsync();
                return responseText;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public async Task<string> GetRequisites(string address)
        {

            try
            {
                // var json = JsonConvert.SerializeObject(commands, Formatting.Indented);
                // StringContent content = new StringContent(json);
                // определяем данные запроса
                var request = new HttpRequestMessage(HttpMethod.Get, address);
                // установка отправляемого содержимого
                //request.Content = content;
                // отправляем запрос
                var response = await Client.GetAsync(address);
                // получаем ответ
                string responseText = await response.Content.ReadAsStringAsync();
                return responseText;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> PutXmlSign(string address, string json, string xmlSign)
        {

            try
            {
                // var json = JsonConvert.SerializeObject(commands, Formatting.Indented);
                var jsonBody = JsonConvert.DeserializeObject<string>(json); 
                StringContent content = new StringContent(json);
                // определяем данные запроса
                var request = new HttpRequestMessage(HttpMethod.Put, address);
                // установка отправляемого содержимого
                request.Content = content;
                // отправляем запрос
                var response = await Client.PutAsync(address,content);
                // получаем ответ
                string responseText = await response.Content.ReadAsStringAsync();
                return responseText;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
