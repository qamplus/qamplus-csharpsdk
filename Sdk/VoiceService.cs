using Newtonsoft.Json;
using QamPlus.Sdk.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QamPlus.Sdk
{

    public class VoiceService: IService
    {        
        public QamPlusClient baseObj;
        public string ApiResource = "voice/v1/{0}";
        public VoiceService(QamPlusClient baseObj)
        {
            this.baseObj = baseObj;
        }


        public async Task<VoiceInitResponse> Create(            
            VoiceRequest request)
        {
            var _apiResource = string.Format(this.ApiResource, request.direction);
            var content = JsonConvert.SerializeObject(request);

            var responseString = await baseObj.Send(HttpMethod.Post, _apiResource, content);
            VoiceInitResponse response = JsonConvert.DeserializeObject<VoiceInitResponse>(responseString);
            return response;
        }

        public async Task<VoiceStatusResponse> GetStatus(string referenceId)
        {
            var _apiResource = string.Format(this.ApiResource, referenceId);
            var responseString = await baseObj.Send(HttpMethod.Get, _apiResource);
            VoiceStatusResponse response = JsonConvert.DeserializeObject<VoiceStatusResponse>(responseString);
            return response;
        }
    }

}
