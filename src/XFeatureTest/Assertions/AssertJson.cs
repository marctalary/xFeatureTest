using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit.Sdk;

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

            if (errors.Any())
                throw new JsonSerializationException<TExpectedDeserializableType>(errors, serializedObjectJson);

            return deserializedObject;
        }
    }

    public class JsonSerializationException<TExpectedDeserializableType> : XunitException
    {
        private readonly List<string> _errors;
        private readonly string _json;

        public JsonSerializationException(List<string> errors, string json) : base("AssertJson.Valid Failure")
        {
            _errors = errors;
            _json = json;
        }

        public override string StackTrace => StackTraceFormatter.RemoveAssertionTraces(base.StackTrace);

        public override string Message
        {
            get
            {
                var jsonErrorsMessage = (_errors?.Count ?? 0) == 0
                    ? ""
                    : $" {Environment.NewLine}Serialization Errors ({_errors.Count}):{Environment.NewLine}{string.Join(Environment.NewLine, _errors)}";

                var mainMessage =
                    $"{Environment.NewLine}Expected Json to be serializable to Type: {typeof(TExpectedDeserializableType).Name}";
                var fullNameMessage =
                    $"{Environment.NewLine}Serialization Type: {typeof(TExpectedDeserializableType).Namespace}.{typeof(TExpectedDeserializableType).Name}";
                var jsonMessage = $"{Environment.NewLine}Json:{Environment.NewLine}{_json}{Environment.NewLine}";

                return string.Concat(Environment.NewLine, UserMessage, Environment.NewLine, mainMessage,
                    jsonErrorsMessage, fullNameMessage, jsonMessage);
            }
        }
    }
}