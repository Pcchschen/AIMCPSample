using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nany.Shared
{    
    public class NanyItemBase<T>: NanyBase
    {

        [JsonProperty(PropertyName = "tagObject")]
        public T? tagObject { get; set; }

    }

    public class NanyBase
    {
        [JsonProperty(PropertyName = "id")]
        public string? id { get; set; }

        /// <summary>
        /// unique for loginId/roleName
        /// </summary>

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "FunctionType")]
        public En_FunctionTypes? FunctionType { get; set; }

        [JsonProperty(PropertyName = "updatedDate")]
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "isDeleted")]
        public bool? Status { get; set; } = false;

        [JsonProperty(PropertyName = "subject")]
        public string? Subject { get; set; }


        [JsonProperty(PropertyName = "updatedBy")]
        public string? UpdatedBy { get; set; }



        [JsonProperty(PropertyName = "VisibleCircleIDs")]
        public List<string>? VisibleCircleIDs { get; set; }


        [JsonProperty(PropertyName = "EntityID")]
        public string? EntityID { get; set; }


    }

}
