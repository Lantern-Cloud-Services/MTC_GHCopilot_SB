using Newtonsoft.Json;

namespace Company.Function
{
    public class Order
    {
        [JsonProperty("id")]
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
