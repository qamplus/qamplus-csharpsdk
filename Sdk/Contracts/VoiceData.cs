using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QamPlus.Sdk.Contracts
{
    public class Status
    {
        public string updated_on { get; set; }
        public int code { get; set; }
        public string description { get; set; }
    }

    public class CodeAndDescription
    {
        public int code { get; set; }

        public string description { get; set; }
    }

    #region Create

    public class VoiceRequest
    {
        public string direction { get; set; }
        public string to { get; set; }
        public string from { get; set; }
        public string country_iso2 { get; set; }
        public string technology { get; set; }

        public VoiceScenario execution_logic { get; set; }

        public VoiceScenario reference_logic { get; set; }

        public string status_callback_uri { get; set; }
    }


    public class VoiceStep
    {   
        public string name { get; set; }
        public string data { get; set; }

    }
    public class VoiceScenario
    {
        public string initial { get; set; }
        public VoiceStep[] steps { get; set; }
    }

    public class VoiceInitResponse
    {
        public Status status { get; set; }
        public List<CodeAndDescription> errors { get; set; }
        public List<CodeAndDescription> warnings { get; set; }
        public string reference_id { get; set; }
        public string formatted_to { get; set; }
        public string formatted_from { get; set; }
        public string resource_http_method { get; set; }
        public string resource_uri { get; set; }
    }
    #endregion

    #region GetStatus
    public class VoiceStatusResponse
    {
        public Status status { get; set; }
        public List<CodeAndDescription> errors { get; set; }
        public string reference_id { get; set; }

        public DateTime? requested_on { get; set; }
        public DateTime? received_on{ get; set; }
        public DateTime? answered_on { get; set; }
        public int? call_duration{ get; set; }
        public double? file_size_ratio{ get; set; }
        public bool? is_matching { get; set; }
        public string resource_http_method { get; set; }
        public string referenced_file_uri { get; set; }
        public string recorded_file_uri { get; set; }
    }
    #endregion
}
