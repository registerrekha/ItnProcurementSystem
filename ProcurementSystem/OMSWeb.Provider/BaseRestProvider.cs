using Itn.Utilities;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Itn.Utilities.Enums;
using Itn.Utilities.Exceptions;

namespace OMSWeb.Provider
{
    public abstract class BaseRestProvider
    {
        #region REST client support methods

        //public string ClientAuthToken()
        //{
        //    return AppConfig.GetConfigVal("Service.Security.Identity.Token");
        //}


        internal T Get<T>(string targetUri)
        {
            //var verbUri = targetUri.ReplaceSidAuthToken(ClientAuthToken());
            var verbUri = targetUri;
            var response = GetClient().GetAsync(verbUri).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<T>().Result;
                return result;
            }
            // We didn't get what we were expecting. There could be many reasons. 
            var unexpectedResult = response.Content.ReadAsStringAsync().Result;
            // we want to check 404 codes specifically.                     
            // If the result is NotFound it could be either 1) we have a bad service route! 2) We have an entity (record) which could not be found.
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // Is the body our response a valid RecordNotFound object? If so, then that's what we want to return. 
                if (unexpectedResult.Contains("RecordNotFound"))
                {
                    throw new RecordNotFoundException();
                }
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedException(unexpectedResult);
            throw new ServiceNotOkException(unexpectedResult, HttpActionServiceResultType.InternalServiceError);
        }

        internal ServicePostResult Post<T>(string targetUri, T data)
        {
           // var verbUri = targetUri.ReplaceSidAuthToken(ClientAuthToken());
            var verbUri = targetUri;
            var response = GetClient().PostAsJsonAsync(verbUri, data).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<ServicePostResult>().Result;
                return result;
            }
            var unexpectedResult = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedException(unexpectedResult);

            throw new ServiceNotOkException(unexpectedResult, HttpActionServiceResultType.InternalServiceError);
        }

        internal ServicePostResult PostWithQueue<T>(string targetUri, T data)
        {
            //var verbUri = targetUri.ReplaceSidAuthToken(ClientAuthToken());
            var verbUri = targetUri;
            var response = GetClient().PostAsJsonAsync(verbUri, data).Result;
            // deserialize json for result
            var resultJson = JsonFormatConverter.Deserialize<AcceptedServiceDataResult>(response.Content.ReadAsStringAsync().Result);
            if (response.IsSuccessStatusCode)
            {
                //var result = response.Content.ReadAsAsync<ServicePostResult>().Result;
                var result = ServicePostResult.Create(resultJson.QueueToken, resultJson.QueueLocation, null);
                return result;
            }
            var unexpectedResult = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedException(unexpectedResult);

            throw new ServiceNotOkException(unexpectedResult, HttpActionServiceResultType.InternalServiceError);
        }



        internal TR Post<T, TR>(string targetUri, T data)
        {
           // var verbUri = targetUri.ReplaceSidAuthToken(ClientAuthToken());
            var verbUri = targetUri;
            var response = GetClient().PostAsJsonAsync(verbUri, data).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<TR>().Result;
                return result;
            }
            var unexpectedResult = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedException(unexpectedResult);
            throw new ServiceNotOkException(unexpectedResult, HttpActionServiceResultType.InternalServiceError);
        }

        private HttpClient GetClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(CoreBaseUri()) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        internal abstract string CoreBaseUri();
       // internal abstract string GetRouteWithSid(string route);


        #endregion
    }
}
