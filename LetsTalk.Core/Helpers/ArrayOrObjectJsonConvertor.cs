using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetsTalk.Core.Helpers;

public class ArrayOrObjectJsonConvertor<T> : JsonConverter<IReadOnlyList<T>>
{


    private record Wrapper(T[] Items);

    public override IReadOnlyList<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.StartArray => JsonSerializer.Deserialize<List<T>>(ref reader, options),
            JsonTokenType.StartObject => JsonSerializer.Deserialize<Wrapper>(ref reader, options).Items,
            _ => throw new JsonException()
        };
    }


    public override void Write(Utf8JsonWriter writer, IReadOnlyList<T> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, options);
    }
}
