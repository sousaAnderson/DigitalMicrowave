using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;

namespace DigitalMicrowave
{
    public static class ApiClient
    {
        public static HttpClient Client()
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", ReadToken());
            return http;
        }
        private static string ReadToken()
        {
            return WebConfigurationManager.AppSettings["JwtToken"];
        }       
    }
}