using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using RssReader.Core.APIs;
using RssReader.Core.Entities;
using RssReader.Core.Services;
using RssReader.Droid.Adapters;

namespace RssReader.Droid.Activities
{
    [Activity(Label = "Détails de la source", Theme = "@style/AppTheme")]
    public class ReadSourceActivity: AppCompatActivity
    {
        private SourceService _sourceService;
        private RecyclerView _itemsRecyclerView;
        private SwipeRefreshLayout _swipeRefreshLayout;
        private Source _item;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            _sourceService = new SourceService(Constants.ConnectionString);
            
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_source_read);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Rss Reader";

            _itemsRecyclerView = FindViewById<RecyclerView>(Resource.Id.rssItems_itemsRecyclerView);
            _swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeToRefresh);
            _itemsRecyclerView.SetLayoutManager(new LinearLayoutManager(this));

            var id = Intent.GetIntExtra(nameof(Constants.READ_SOURCE_ID), -1);

            if (id > -1)
            {
                _item = _sourceService.GetById(id);

                _swipeRefreshLayout.Refresh += Refresh;

                var _ = Load();
            }
            
        }

        private async void Refresh(object sender, System.EventArgs e)
        {
            await Load();
        }

        private async Task Load()
        {
            _swipeRefreshLayout.Refreshing = true;

            var items = await new RssApi().GetAsync(_item.Uri);
            var rssItemAdapter = new RssItemAdapter(this, items);
            _itemsRecyclerView.SetAdapter(rssItemAdapter);

            _swipeRefreshLayout.Refreshing = false;
        }

        public override bool OnOptionsItemSelected(IMenuItem? item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}