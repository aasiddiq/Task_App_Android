using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Org.Json;
using RestSharp.Deserializers;
using RestSharp;
using System.Threading;

namespace App2
{
    [Activity(Label = "MovieActivity")]
    public class MovieActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Movies);
            Button getMovies = FindViewById<Button>(Resource.Id.btnGetMovieList);
            var listView = FindViewById<ListView>(Resource.Id.lstMovies);
            getMovies.Click += (object sender, EventArgs e) =>
                {
                    ProgressDialog progress = ProgressDialog.Show(this, "Please wait", "Contacting Server", true, false);
                    new Thread(new ThreadStart(delegate
                        {
                            LoadListView();
                            RunOnUiThread(() => progress.Dismiss());
                        })).Start();
                };
        }

        private void LoadListView()
        {
            var result = App2.CallApi.getMovies();
            JsonDeserializer deserial = new JsonDeserializer();
            var movieList = deserial.Deserialize<List<Movie>>(result);
            RunOnUiThread(() =>
            {
                var listView = FindViewById<ListView>(Resource.Id.lstMovies);
                listView.Adapter = new MovieAdapter(this, movieList);
            });
        }
    }
}

