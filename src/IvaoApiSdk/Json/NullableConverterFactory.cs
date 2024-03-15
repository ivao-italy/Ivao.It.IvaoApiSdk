﻿using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ivao.It.IvaoApiSdk.Json;
public class NullableConverterFactory : JsonConverterFactory
{
    static readonly byte[] Empty = Array.Empty<byte>();

    public override bool CanConvert(Type typeToConvert) => Nullable.GetUnderlyingType(typeToConvert) != null;

    public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
    {
        var t = Nullable.GetUnderlyingType(type) ?? throw new JsonException($"Unable to convert type: {type}");

        var instance = Activator.CreateInstance(
            typeof(NullableConverter<>).MakeGenericType(new Type[] { t! }),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: new object[] { options },
            culture: null);
        
        return instance is null ? throw new JsonException($"Unable to convert type: {type}") : (JsonConverter)instance;
    }

    class NullableConverter<T> : JsonConverter<T?> where T : struct
    {
        // DO NOT CACHE the return of (JsonConverter<T>)options.GetConverter(typeof(T)) as DoubleConverter.Read() and DoubleConverter.Write()
        // DO NOT WORK for nondefault values of JsonSerializerOptions.NumberHandling which was introduced in .NET 5
        public NullableConverter(JsonSerializerOptions options) { }

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (reader.ValueTextEquals(Empty))
                    return null;
            }
            return JsonSerializer.Deserialize<T>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options) =>
            JsonSerializer.Serialize(writer, value!.Value, options);
    }
}
