using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace QamPlus.Sdk
{
    public class Response
    {
        public string StatusCode;
        public string StatusMessage;

        public Dictionary<string, string> Values;

        public Response(dynamic apiResponse)
        {
            for(int i=0; i < apiResponse.Items.Length; i++)
            {
                if(apiResponse[i].Key == "status")
                {
                    this.StatusCode = apiResponse[i].Value;
                }
                else
                {
                    Values.Add(apiResponse[i].Key, apiResponse[i].Value);
                }
            }
        }
    }

    public class QamPlusClient
    {
        public string CustomerId { get; set; }

        public string Password { get; set; }

        public string PublicUrl { get; set; }

        public string Version { get; set; }

        public VoiceService Voice; 
        //public QamPlusClient Sms;
        //public QamPlusClient Email;
        //public QamPlusClient Api;

        public QamPlusClient(string customerId, string password, string url="https://api.qamplus.com")
        {
            this.CustomerId = customerId;
            this.Password = password;
            this.PublicUrl = url;
            this.Version = "v1";

            this.Voice = new VoiceService(this);
        }

        public async Task<string> Send(HttpMethod httpMethod, string apiResource, string content = null)
        {
            string resourceUri = string.Format("{0}/{1}", this.PublicUrl, apiResource);
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(this.CustomerId + ":" + this.Password));

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                var message = new HttpRequestMessage(httpMethod, string.Format(resourceUri));
                message.Headers.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);

                if (httpMethod != HttpMethod.Get || content != null)
                    message.Content = new StringContent(content, Encoding.UTF8, "application/json");
                                
                var httpResponse = await httpClient.SendAsync(message);
                if(!httpResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(string.Format("Error connecting to QAMplus api at {0}. Status code: {1}; Reason phrase: {2}", 
                        resourceUri, httpResponse.StatusCode, httpResponse.ReasonPhrase));
                }
                string sContent = await httpResponse.Content.ReadAsStringAsync();
                return sContent;
            }
        }
    }
}
