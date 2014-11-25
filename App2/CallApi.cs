using System;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;

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
                    return resp.ErrorException.ToString()+resp.ErrorMessage;
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
            request.RequestFormat = DataFormat.Json;
            //try
            //{
            IRestResponse resp = client.Execute(request);
            // if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            var content = resp.Content;
            //JsonDeserializer deserial = new JsonDeserializer();
            //List<Movie> movieList = deserial.Deserialize<List<Movie>>(resp);
            //var JSONObj = deserial.Deserialize<Dictionary<string, string>>(resp);
            //int rowCount = JSONObj["Count"];
            // DeserializeRxTerm(resp.Content);
            return resp;
            //}
            //else
            // return resp.ErrorMessage;
            //}
            //catch (Exception ex)
            //{
            //    return "Try Again";
            //}
        }
    }
}

