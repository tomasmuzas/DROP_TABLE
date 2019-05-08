using Newtonsoft.Json;

namespace BackendApartmentReservation.Extensions.Json
{
    public static class JsonSerializationExtensions
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            MaxDepth = 2
        };

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Settings);
        }
    }
}
