using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using RssReader.Core.Entities;

namespace RssReader.Droid.Adapters
{
    public class RssItemAdapter: RecyclerView.Adapter
    {
        public class RssItemViewHolder : RecyclerView.ViewHolder
        {
            public RssItemViewHolder(View view): base(view)
            {
                
            }
        }
        
        private Activity _context;
        private List<RssItem> _elements;
        private TextView _textviewDescription;
        private ImageView _imageView;
        private TextView _textviewTitle;

        public RssItemAdapter(Activity context, List<RssItem> elements)
        {
            _elements = elements;
            _context = context;
        }

        public override int ItemCount => _elements.Count;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = _context.LayoutInflater.Inflate(Resource.Layout.viewHolder_rssItem, parent, false);
            return new RssItemViewHolder(view);
        }
        
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var rssItemViewHolder = holder as RssItemViewHolder;
            _textviewTitle = rssItemViewHolder.ItemView.FindViewById<TextView>(Resource.Id.rssitem_title);
            _textviewDescription = rssItemViewHolder.ItemView.FindViewById<TextView>(Resource.Id.rssitem_description);
            _imageView = rssItemViewHolder.ItemView.FindViewById<ImageView>(Resource.Id.rssitem_image);

            _textviewTitle.Text = _elements[position].Title;
            _textviewDescription.Text = _elements[position].Description;

            ImageService.
                Instance
                .LoadUrl(_elements[position].ImageUrl)
                .LoadingPlaceholder("loadingplaceholder", FFImageLoading.Work.ImageSource.CompiledResource)
                .ErrorPlaceholder("errorplaceholder", FFImageLoading.Work.ImageSource.CompiledResource)
                .Into(_imageView);

            if (!string.IsNullOrWhiteSpace(_elements[position].ImageUrl))
            {
                rssItemViewHolder.ItemView.Click += (sender, args) =>
                {
                    var intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(_elements[position].ImageUrl));
                _context.StartActivity(intent);
                };
            }

        }

    }
}