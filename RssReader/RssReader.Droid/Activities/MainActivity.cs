using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RssReader.Core.Entities;
using RssReader.Core.Services;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;

namespace RssReader.Droid.Activities
{
    public class SourceAdapter: BaseAdapter<Source>
    {
        private readonly Activity _context;
        private readonly List<Source> _elements;

        public SourceAdapter(Activity context, List<Source> elements)
        {
            _context = context;
            _elements = elements;
        }

        public override Source this[int position] => _elements.ElementAtOrDefault(position);

        public override long GetItemId(int position)
        {
            return (long)_elements.ElementAtOrDefault(position)?.Id;
        }

        public override View GetView(int position, View? convertView, ViewGroup? parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, parent, false);

            var textview1 = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            var textview2 = view.FindViewById<TextView>(Android.Resource.Id.Text2);

            textview1.Text = _elements.ElementAtOrDefault(position)?.Title;
            textview2.Text = $"Created at {_elements.ElementAtOrDefault(position).CreatedAt:dd/MM/yyyy}";

            return view;
        }

        public override int Count { get => _elements.Count(); }

        public void AddAndRefresh(Source item)
        {
            _elements.Insert(0, item);
            NotifyDataSetChanged();
        }
    }
    
    [Activity(Label = "Rss Reader", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private readonly SourceService _sourceService = new SourceService(Constants.ConnectionString);

        private RelativeLayout _rootView;
        private ListView _listviewSource;
        private FloatingActionButton  _btnAdd;
        private SourceAdapter _sourceAdapter;
        private DrawerLayout _drawer;
        private NavigationView _navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            _navigationView = FindViewById<NavigationView>(Resource.Id.main_navigationview);
            _drawer = FindViewById<DrawerLayout>(Resource.Id.main_drawer);

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Rss Reader";

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Android.Resource.Drawable.IcMenuMore);

            _rootView = FindViewById<RelativeLayout>(Resource.Id.rootView);
            _btnAdd = FindViewById<FloatingActionButton>(Resource.Id.main_button_add);

            if (_btnAdd != null)
            {
                _btnAdd.Click += BtnAddOnClick;
            }
            
            _listviewSource = FindViewById<ListView>(Resource.Id.main_listview_source);
            _sourceAdapter = new SourceAdapter(this, this._sourceService.Get());

            if (_listviewSource != null)
            {
                _listviewSource.Adapter = _sourceAdapter;
                _listviewSource.ItemClick += ListviewSourceOnItemClick;
            }

            _navigationView.NavigationItemSelected += _navigationView_NavigationItemSelected;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private void _navigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            if(e.MenuItem.ItemId == Resource.Id.navmenu_add)
            {
                var intent = new Intent(this, typeof(AddActivity));
                StartActivity(intent);
            }

            _drawer.CloseDrawer(_navigationView);
        }

        private void ListviewSourceOnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(ReadSourceActivity));
            intent.PutExtra(nameof(Constants.READ_SOURCE_ID), _sourceAdapter[e.Position].Id);
            StartActivity(intent);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case Constants.CREATE_SOURCE_ID when resultCode ==  Result.Ok:
                    var id = data.GetIntExtra(nameof(Constants.CREATE_SOURCE_ID), -1);
                    // Toast.MakeText(this, $"Result : { resultCode }, id result : {id}", ToastLength.Short).Show();
                    var matchingSource = _sourceService.GetById(id);
                    _sourceAdapter.AddAndRefresh(matchingSource);
                    Snackbar
                        .Make(_rootView, _rootView.Id, Snackbar.LengthLong)
                        .SetText("Source de flux ajoutée!")
                        .SetAction("Ok", view => {})
                        .Show();
                    break;
                default: break;
            }
        }        

        private void BtnAddOnClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AddActivity));
            StartActivityForResult(intent, Constants.CREATE_SOURCE_ID);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool OnOptionsItemSelected(IMenuItem? item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                _drawer.OpenDrawer(_navigationView);
                return true;
            }

            if (item.ItemId == Resource.Id.main_menu_add)
            {
                var intent = new Intent(this, typeof(AddActivity));
                StartActivityForResult(intent, Constants.CREATE_SOURCE_ID);
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}