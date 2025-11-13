using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;

namespace DigitalMicrowave
{
    public static class ApiHelper
    {
        public static HttpClient GetHttpClient()
        {
            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"];
            var client = new HttpClient { BaseAddress = new Uri(apiUrl) };

            var context = HttpContext.Current;
            if (context != null && context.Session != null)
            {
                var token = HttpContext.Current.Session["AuthToken"] as string;
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }
            }           

            return client;
        }
    }
}