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

namespace App2
{
    public class MovieAdapter: BaseAdapter<Movie>
    {
        List<Movie> items;
        Activity context;

        public MovieAdapter(Activity context, List<Movie> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override Movie this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.ListTemplate, null);
            }
            view.FindViewById<ImageView>(Resource.Id.thumbnail).SetImageResource(Resource.Drawable.Icon);
            view.FindViewById<TextView>(Resource.Id.txtTitle).Text = item.title;
            view.FindViewById<TextView>(Resource.Id.txtLang).Text = item.language;
            int min = item.durationIsSecs / 60;
            string sec = (item.durationIsSecs % 60).ToString();
            if (int.Parse(sec) < 10)
                sec = "0" + sec;
            view.FindViewById<TextView>(Resource.Id.txtDuration).Text="Duration: "+min.ToString()+":"+sec+" Mins";

            return view;
        }
    }
}
