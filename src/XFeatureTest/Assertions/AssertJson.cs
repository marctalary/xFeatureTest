using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace XFeatureTest.Assertions
{
    public static class AssertJson
    {
        public static TExpectedDeserializableType Valid<TExpectedDeserializableType>(string serializedObjectJson)
        {
            var errors = new List<string>();
            var serializerSettings = new JsonSerializerSettings
            {
                Error = delegate(object sender, ErrorEventArgs args)
                {
                    errors.Add(args.ErrorContext.Error.Message);
                    args.ErrorContext.Handled = true;
                }
            };

            var deserializedObject =
                JsonConvert.DeserializeObject<TExpectedDeserializableType>(serializedObjectJson, serializerSettings);

            if (errors.Any()) Assert.True(false, $"Not valid json. Errors: {string.Join(", ", errors)}");

            return deserializedObject;
        }
    }
}