using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace XFeatureTest.Assertions
{
    public static class AssertHttp
    {
        /// <summary>
        ///     Assert status code is equal to response status code and includes the content if the assertion is false
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actualResponse"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> StatusCode(HttpStatusCode expected,
            HttpResponseMessage actualResponse)
        {
            if (actualResponse.StatusCode != expected)
                throw new HttpStatusException(expected, actualResponse.StatusCode,
                    await GetContentAsString(actualResponse));

            return actualResponse;
        }

        public static void StatusCode(HttpStatusCode expected, HttpStatusCode actual)
        {
            if (actual != expected)
                //    throw new TestAssertActualExpectedException(expected, actual, "TestUserMessage");
                throw new HttpStatusException(expected, actual);
        }

        private static async Task<string> GetContentAsString(HttpResponseMessage response)
        {
            var responseData = "";

            try
            {
                responseData = await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                // ignored
            }

            return responseData;
        }

        public class HttpStatusException : AssertActualExpectedException
        {
            private readonly string _responseContent;

            public HttpStatusException(HttpStatusCode expected, HttpStatusCode actual, string responseContent = null)
                : base($"{(int)expected} {expected}", $"{(int)actual} {actual}", "AssertHttp.StatusCode Failure",
                    "Expected HTTP", "Actual HTTP")
            {
                _responseContent = responseContent;
            }

            public override string StackTrace => StackTraceFormatter.RemoveAssertionTraces(base.StackTrace);

            public override string Message
            {
                get
                {
                    var contentDescription = string.IsNullOrWhiteSpace(_responseContent)
                        ? ""
                        : $" {Environment.NewLine}Response Content:{Environment.NewLine}{_responseContent}{Environment.NewLine}";

                    return $"{base.Message}{contentDescription}";
                }
            }
        }
    }
}