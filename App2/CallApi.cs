using System;
using RestSharp;

namespace App2
{
    public class CallApi
    {
        public static string Fetch(string Name, String Location)
        {
            var client = new RestClient("http://task-host.azurewebsites.net");
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
                    return resp.ErrorMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}

