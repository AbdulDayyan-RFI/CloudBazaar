using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TEB.Core.Common;

namespace TEB.Web.Controllers
{
    public class BaseController : Controller
    {
        string apival = ConfigurationManager.AppSettings["ApiUrl"];

        /// <summary>
        /// get method request to api
        /// </summary>
        /// <param name="url">relative url of the resource</param>
        public async Task<TEBApiResponse> Get(string url)
        {
            var apiResponse = new TEBApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apival);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.GetAsync("api" + url);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<TEBApiResponse>();

                return apiResponse;
            }
        }

        /// <summary>
        /// post method request to api
        /// </summary>
        /// <param name="url">relative url of the resource</param>
        /// <param name="formdata">post data in dictionary format</param>
        /// <returns></returns>
        protected virtual async Task<string> Post(string url, Dictionary<string, string> formdata)
        {

            if (formdata == null)
                return string.Empty;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apival);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                var postData = new List<KeyValuePair<string, string>>();

                foreach (var param in formdata)
                    postData.Add(new KeyValuePair<string, string>(param.Key, param.Value));

                HttpContent content = new FormUrlEncodedContent(postData);
                HttpResponseMessage response = await client.PostAsync("api" + url, content);

                string apiResponse = string.Empty;
                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsStringAsync();

                return apiResponse;
            }
        }

        /// <summary>
        /// post method request to api
        /// </summary>
        /// <param name="url">relative url of the resource</param>
        /// <param name="formdata">generic form data to post</param>
        /// <returns>NECApiResponse</returns>
        protected virtual async Task<TEBApiResponse> Post<T>(string url, T formdata)
        {
            if (formdata == null)
                throw new ArgumentException("cannot create post request without formdata");

            var apiResponse = new TEBApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(apival);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.PostAsJsonAsync("api" + url, formdata);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<TEBApiResponse>();

                return apiResponse;
            }
        }

        /// <summary>
        /// put method request to api
        /// </summary>
        /// <param name="url">relative url of the resource</param>
        /// <param name="formdata">generic form data to update</param>
        /// <returns>NECApiResponse</returns>
        protected virtual async Task<TEBApiResponse> Put<T>(string url, T formdata)
        {
            if (formdata == null)
                throw new ArgumentException("cannot create post request without formdata");

            var apiResponse = new TEBApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apival);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.PutAsJsonAsync("api" + url, formdata);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<TEBApiResponse>();

                return apiResponse;
            }
        }

        /// <summary>
        /// Delete method request to api
        /// </summary>
        /// <param name="url">relative url of the resource</param>
        /// <param name="formdata">generic form data to update</param>
        /// <returns>NECApiResponse</returns>
        protected virtual async Task<TEBApiResponse> Delete(string url)
        {
            var apiResponse = new TEBApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apival);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.DeleteAsync("api" + url);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<TEBApiResponse>();

                return apiResponse;
            }
        }

        // Handy helper method to set the access token for each request:
        void SetClientAuthentication(HttpClient client)
        {
            //var token =  HttpContext.User.Identity.GetToken();

            //if (string.IsNullOrWhiteSpace(token))
            //    return;

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}