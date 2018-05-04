using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApp.WebUI.Models
{
    /// <summary>
    /// 
    /// </summary>

    public class User 
    {
        [JsonProperty(PropertyName = "guid")]
        public string Guid { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        public Boolean IsVoted { get; set; } = false;
    }
}
