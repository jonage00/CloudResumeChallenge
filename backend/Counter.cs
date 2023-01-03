using Newtonsoft.Json;

namespace CosmosDBFunction

{
    public class Counter 
    {
        [JsonProperty(PropertyName="id")]
        public string Id {get; set;}
        [JsonProperty(PropertyName = "count")]
        public int Count {get;set;} 
    }
}