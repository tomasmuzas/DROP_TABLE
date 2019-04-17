namespace BackendApartmentReservation.Extensions.Json
{
    using Newtonsoft.Json;

    public static class JsonSerializationExtensions
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Settings);
        }
    }
}