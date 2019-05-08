namespace BackendApartmentReservation.Infrastructure.Extensions.Json
{
    using Newtonsoft.Json;

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
