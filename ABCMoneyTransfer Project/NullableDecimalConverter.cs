using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ABCMoneyTransfer_Project
{
    // ----------------------------------------------------------------------------------------------------------------------------------
    public class NullableDecimalConverter : JsonConverter<decimal?>
    {
        public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (decimal.TryParse(stringValue, out var value))
                {
                    return value;
                }
                return null;
            }
            else if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            throw new JsonException("Invalid value for decimal?");
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString());
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }

    // ----------------------------------------------------------------------------------------------------------------------------------
}