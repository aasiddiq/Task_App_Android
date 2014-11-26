using System;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace App2
{
    public class CallApi
    {
        static RestClient client = new RestClient("http://task-host.azurewebsites.net");

        public static string Fetch(string Name, String Location)
        {
            string api = "api/person/" + Name + "/hello/" + Location;
            RestRequest request = new RestRequest(api);
            try
            {
                IRestResponse resp = client.Execute(request);
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = resp.Content.Trim('"');
                    return content;
                }
                else
                    return resp.ErrorException.ToString() + resp.ErrorMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static IRestResponse getMovies()
        {
            string api = "api/movie/list";
            RestRequest request = new RestRequest(api);
            request.AddHeader("px-hash", request.AddHash(""));
            request.RequestFormat = DataFormat.Json;
            IRestResponse resp = client.Execute(request);
            var content = resp.Content;
            return resp;
        }
    }
    public static class ExtensionMethods
    {
        public static string AddHash(this RestRequest RequestObject, string SharedKey)
        {
            var parameters = RequestObject.Parameters.OrderBy(p => p.Name).ToList();
            string[] paramType = { "QueryString", "RequestBody", "HttpHeader", "Cookie" };
            foreach (string type in paramType)
            {
                SharedKey = appendHashString(SharedKey, type, parameters);
                //For the route part We can add this.
                //if (type == paramType[1])
                //    SharedKey += RequestObject.Resource;
            }
            UnicodeEncoding ue = new UnicodeEncoding();
            byte[] HashValue;
            byte[] sharedkey = ue.GetBytes(SharedKey);
            SHA256Managed sha256 = new SHA256Managed();
            string hashString = string.Empty;
            HashValue = sha256.ComputeHash(sharedkey);
            foreach (byte b in HashValue)
            {
                hashString += string.Format("{0:X2}", b);
            }
            return hashString;
        }

        private static string appendHashString(string SharedKey, string type, List<Parameter> parameters)
        {
            var param = from p in parameters where p.Type.ToString() == type select p;
            foreach (var item in param)
            {
                SharedKey += item.Name + item.Value;
            }
            return SharedKey;
        }
    }
}

