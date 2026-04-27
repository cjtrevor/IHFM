using Newtonsoft.Json;

namespace IHFM.VAF.Resident.Models
{
    /// <summary>
    /// Represents a single object that needs its Site property updated
    /// as a result of a Resident's site change.
    /// </summary>
    public class ResidentSiteChangeQueueItem
    {
        //PRAN NOTES
        //Will need to have classIds to have proper control over which objects to update/search

        [JsonProperty("objId")]
        public int ObjId { get; set; }

        [JsonProperty("objType")]
        public int ObjType { get; set; }

        [JsonProperty("newSiteId")]
        public int NewSiteId { get; set; }

        [JsonProperty("residentObjId")]
        public int ResidentObjId { get; set; }
    }
}
