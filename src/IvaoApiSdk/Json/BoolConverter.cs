using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ivao.It.IvaoApiSdk.Json;
internal class BoolConverter : JsonConverter<bool>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return base.CanConvert(typeToConvert);
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
        writer.WriteBooleanValue(value);

    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType switch
        {
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.String => bool.TryParse(reader.GetString(), out var b) ? b : throw new JsonException("BoolConverter: String token not compatible with bool value"),
            JsonTokenType.Number => reader.TryGetInt64(out long l) ? Convert.ToBoolean(l) : reader.TryGetDouble(out double d) && Convert.ToBoolean(d),
            _ => throw new JsonException(),
        };
}