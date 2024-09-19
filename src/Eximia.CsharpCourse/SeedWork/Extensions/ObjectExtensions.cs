using Newtonsoft.Json;

namespace System
{
    public static class ObjectExtensions
    {
        public static string ToNameTypeJson(this object @object)
            => JsonConvert.SerializeObject(@object, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

        public static string ToJson(this object @object)
            => JsonConvert.SerializeObject(@object, new JsonSerializerSettings { });

        public static TTipoDesejado ToNameTypeObject<TTipoDesejado>(this string json)
            => JsonConvert.DeserializeObject<TTipoDesejado>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects })!;

        public static TTipoDesejado ToObject<TTipoDesejado>(this string json)
            => JsonConvert.DeserializeObject<TTipoDesejado>(json, new JsonSerializerSettings { })!;

        public static async Task<TTipoDesejado> ToObjectAsync<TTipoDesejado>(this Stream stream, CancellationToken cancellationToken)
        {
            using var json = new StreamReader(stream);
            return JsonConvert.DeserializeObject<TTipoDesejado>(await json.ReadToEndAsync(cancellationToken).ConfigureAwait(false))!;
        }

        public static bool In<T>(this T item, params T[] items)
            => items.Contains(item);
    }
}
