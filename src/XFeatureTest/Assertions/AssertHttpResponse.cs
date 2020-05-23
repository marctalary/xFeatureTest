using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XFeatureTest.Assertions
{
    public static class AssertHttpResponse
    {
        /// <summary>
        ///     Assert status code is equal to response status code and includes the content if the assertion is false
        /// </summary>
        /// <param name="expectedStatusCode"></param>
        /// <param name="actualResponse"></param>
        /// <returns></returns>
        public static async Task StatusCodeEquals(HttpStatusCode expectedStatusCode, HttpResponseMessage actualResponse)
        {
            if (actualResponse.StatusCode != expectedStatusCode)
            {
                var content = await GetContentAsString(actualResponse);
                content = string.IsNullOrWhiteSpace(content) ? "" : $" with response content = {content}";
                Assert.True(false,
                    $"Expected {(int) expectedStatusCode} {expectedStatusCode} http status code but found {(int) actualResponse.StatusCode} {actualResponse.StatusCode}{content}");
            }
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
    }
}