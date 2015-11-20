using System;
using CountingCalories.Models;
using CountingCalories.Utils;
using Newtonsoft.Json;

namespace CountingCalories.Converteres
{
    public class LinkModelJsonConverter : JsonConverter
    {
        private static readonly string HrefPropertyName = ExpressionProcessor.GetPropertyName(() => ((LinkModel) null).Href),
                                       RelPropertyName = ExpressionProcessor.GetPropertyName(() => ((LinkModel) null).Rel),
                                       MethodPropertyName = ExpressionProcessor.GetPropertyName(() => ((LinkModel) null).Method),
                                       IsTemplatePropertyName = ExpressionProcessor.GetPropertyName(() => ((LinkModel) null).IsTempleted);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var model = (LinkModel)value;
            writer.WriteStartObject();
            writer.WritePropertyName(HrefPropertyName);

            writer.WriteValue(model.Href);
            writer.WritePropertyName(RelPropertyName);
            writer.WriteValue(model.Rel);

            if (!string.Equals(model.Method, "GET", StringComparison.InvariantCultureIgnoreCase))
            {
                writer.WritePropertyName(MethodPropertyName);
                writer.WriteValue(model.Method);
            }

            if (model.IsTempleted)
            {
                writer.WritePropertyName(IsTemplatePropertyName);
                writer.WriteValue(model.IsTempleted);
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LinkModel);
        }
    }
}